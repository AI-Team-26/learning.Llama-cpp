#!/usr/bin/env python3
"""
Step 1: download the latest llama.cpp Windows x64 CUDA 12 build and the matching
CUDA runtime (cudart) DLLs from https://github.com/ggml-org/llama.cpp/releases

Files downloaded (example for tag b11060):
  llama-b11060-bin-win-cuda-12.4-x64.zip
  cudart-llama-bin-win-cuda-12.4-x64.zip

Usage:
    python download_llama_cpp.py                     # -> D:/Downloads
    python download_llama_cpp.py --dest E:/Somewhere
    python download_llama_cpp.py --dry-run           # only show what would be downloaded

Optional: set GITHUB_TOKEN to avoid GitHub API rate limits.
"""
import argparse
import hashlib
import json
import os
import re
import sys
import urllib.error
import urllib.request
from pathlib import Path

REPO = "ggml-org/llama.cpp"
API_URL = f"https://api.github.com/repos/{REPO}/releases?per_page=30"
FEED_URL = f"https://github.com/{REPO}/releases.atom"
TAG_RE = re.compile(r"^b(\d+)$")  # llama.cpp build tags, e.g. b11060
DEFAULT_DEST = "D:/Downloads"
FALLBACK_CUDA = "12.4"  # only used if the GitHub API is unavailable



def http_open(url, accept="*/*", method="GET"):
    headers = {"User-Agent": "llama-cpp-downloader", "Accept": accept}
    token = os.environ.get("GITHUB_TOKEN")
    if token and "api.github.com" in url:
        headers["Authorization"] = f"Bearer {token}"
    return urllib.request.urlopen(urllib.request.Request(url, headers=headers, method=method))


def version_key(v):
    return tuple(int(x) for x in v.split("."))


def cuda12_version(name, prefix):
    """'llama-b11060-bin-win-cuda-12.4-x64.zip' -> '12.4'. None if not a Windows CUDA 12 x64 zip."""
    if name.startswith(prefix) and "-bin-win-cuda-12" in name and name.endswith("-x64.zip"):
        return name.split("-bin-win-cuda-")[1].removesuffix("-x64.zip")


def pick_assets(assets):
    """Return (main_zip, cudart_zip), preferring the same, highest CUDA 12.x version."""
    mains = {v: a for a in assets if (v := cuda12_version(a["name"], "llama-"))}
    cudarts = {v: a for a in assets if (v := cuda12_version(a["name"], "cudart-"))}
    if not mains:
        sys.exit("No Windows CUDA 12 llama.cpp asset found in the latest release.")
    common = sorted(set(mains) & set(cudarts), key=version_key)
    if common:
        return mains[common[-1]], cudarts[common[-1]]
    print("Warning: no cudart build matches the main build's CUDA version.")
    main = mains[max(mains, key=version_key)]
    cudart = cudarts[max(cudarts, key=version_key)] if cudarts else None
    return main, cudart


def latest_release_via_api():
    """Newest bNNNNN release that has the Windows CUDA 12 assets.

    Note: GitHub's /releases/latest is NOT usable here - the bNNNNN builds are
    published as pre-releases, so 'latest' points at an unrelated release.
    """
    with http_open(API_URL, accept="application/vnd.github+json") as resp:
        releases = json.load(resp)
    # Look at several releases, not just the newest: it may be incomplete (still uploading),
    # and the list can contain non-build releases such as "v0.4.1".
    builds = [r for r in releases if TAG_RE.match(r["tag_name"]) and not r["draft"]]
    for rel in sorted(builds, key=lambda r: int(r["tag_name"][1:]), reverse=True):
        try:
            main, cudart = pick_assets(rel["assets"])
        except SystemExit:
            continue  # this build has no Windows CUDA 12 zip (e.g. still uploading)
        return rel["tag_name"], [a for a in (main, cudart) if a]
    sys.exit("No recent llama.cpp release with Windows CUDA 12 assets found.")


def latest_release_via_feed():
    """No-API fallback: read the newest bNNNNN tag from the releases Atom feed."""
    with http_open(FEED_URL) as resp:
        feed = resp.read().decode("utf-8", "replace")
    tags = [int(m) for m in re.findall(r"/releases/tag/b(\d+)", feed)]
    if not tags:
        sys.exit("Could not determine the latest tag from the releases feed.")
    tag = f"b{max(tags)}"
    base = f"https://github.com/{REPO}/releases/download/{tag}"
    names = [
        f"llama-{tag}-bin-win-cuda-{FALLBACK_CUDA}-x64.zip",
        f"cudart-llama-bin-win-cuda-{FALLBACK_CUDA}-x64.zip",
    ]
    return tag, [{"name": n, "size": None, "digest": None, "browser_download_url": f"{base}/{n}"} for n in names]


def get_latest_release():
    try:
        return latest_release_via_api()
    except urllib.error.HTTPError as e:
        print(f"GitHub API unavailable (HTTP {e.code}); falling back to the releases feed.")
        return latest_release_via_feed()


def sha256_of(path):
    h = hashlib.sha256()
    with open(path, "rb") as f:
        while chunk := f.read(1 << 20):
            h.update(chunk)
    return h.hexdigest()


def verify(path, digest):
    if digest and digest.startswith("sha256:"):
        if sha256_of(path) != digest.split(":", 1)[1]:
            path.unlink()
            sys.exit(f"Checksum mismatch for {path.name}; file removed.")
        print("  sha256 OK")


def download(asset, dest_dir):
    target = dest_dir / asset["name"]
    size = asset.get("size")
    if size and target.exists() and target.stat().st_size == size:
        print(f"Already downloaded: {target}")
        return target

    tmp = target.with_name(target.name + ".part")
    print(f"Downloading {asset['name']}")
    with http_open(asset["browser_download_url"]) as resp, open(tmp, "wb") as f:
        total = size or int(resp.headers.get("Content-Length") or 0)
        done = 0
        while chunk := resp.read(1 << 20):
            f.write(chunk)
            done += len(chunk)
            if total:
                print(f"\r  {done / total:6.1%}  ({done / 1e6:.0f}/{total / 1e6:.0f} MB)", end="", flush=True)
    print()
    tmp.replace(target)
    verify(target, asset.get("digest"))
    return target


def main():
    ap = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("--dest", default=DEFAULT_DEST, help=f"download folder (default: {DEFAULT_DEST})")
    ap.add_argument("--dry-run", action="store_true", help="show what would be downloaded and exit")
    args = ap.parse_args()

    tag, assets = get_latest_release()
    print(f"Latest release: {tag}")
    for a in assets:
        print(f"  {a['name']}\n    {a['browser_download_url']}")
    if args.dry_run:
        return

    dest = Path(args.dest)
    dest.mkdir(parents=True, exist_ok=True)
    print(f"Saving to: {dest.resolve()}")
    for a in assets:
        download(a, dest)
    print("Done.")


if __name__ == "__main__":
    main()
