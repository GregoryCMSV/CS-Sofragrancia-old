namespace Sofragrancia.UI.Utils
{
    public static class CssUtils
    {
        public static string GetCorHexadecimal(string cssClass)
        {
            return cssClass switch
            {
                "success" => "#2ecc71",
                "warning" => "#e6a817",
                "danger" => "#e74c3c",
                _ => "#3498db"
            };
        }

        public static string GetBarColorClass(string cssClass)
        {
            return cssClass switch
            {
                "success" => "bar-green",
                "warning" => "bar-orange",
                "danger" => "bar-red",
                _ => "bar-blue"
            };
        }
    }
}
