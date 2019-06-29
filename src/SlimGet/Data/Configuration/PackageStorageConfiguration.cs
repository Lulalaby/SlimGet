﻿namespace SlimGet.Data.Configuration
{
    public class PackageStorageConfiguration
    {
        public bool EnablePruning { get; set; }
        public int LatestVersionRetainCount { get; set; }
        public long MaxPackageSizeBytes { get; set; }
    }
}