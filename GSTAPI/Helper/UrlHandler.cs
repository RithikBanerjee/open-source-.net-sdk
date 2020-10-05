namespace GSTAPI.Helper
{
    //helper to manage gst portal address
    internal static class UrlHandler
    {
        private static string Domain = "devapi.gst.gov.in";
        public static string Route(accessGroup accessGroup, version version, modName modName)
        {
            return $"https://{Domain}/{accessGroup}/{GetVersion(version)}/{GetModName(modName)}";
        }
        public static string GetVersion(version version)
        {
            return version.ToString().Replace("_", ".");
        }
        private static string GetModName(modName modName)
        {
            return modName.ToString().Replace("_", "/");
        }
    }
    public enum accessGroup { taxpayerapi, commonapi }
    public enum version { v0_1, v0_2, v0_3, v1_0, v1_1, v1_2 }
    public enum modName { returns_gstr1, returns_gstr2a, returns_gstr3b, returns_gstr4a, returns_gstr4, returns_gstr7, returns_gstr9, returns_gstr9c, returns_itc04, returns_gstr, cmp, document, returns, search, authenticate, ledgers }
}
