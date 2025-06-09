namespace Proyecto_Software_Individual.Aplication.Utils
{
    public static class Helper
    {

        public static bool CheckIfNull<T>(T model)
        {
            return model == null;
        }

        public static bool CheckIfListIsNull<T>(List<T>? model)
        {
            return model == null || model.Count == 0;
        }
    }
}
