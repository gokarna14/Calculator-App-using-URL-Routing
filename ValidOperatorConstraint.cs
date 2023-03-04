namespace Calculator
{
    public class ValidOperatorConstraint: IRouteConstraint
    {
        private static string[] ValidOperators = {"+", "-", "/", "*"};

        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string segmentValue = values[routeKey] as string ?? "";

            return ValidOperators.Contains(segmentValue);
        }
    }
}