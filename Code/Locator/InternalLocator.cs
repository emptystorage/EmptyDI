using System;

namespace EmptyDI.Code.Locator
{
    internal interface ILocatorObject { }

    internal static class InternalLocator
    {
        internal static void SetObject<T>(T @object)
            where T: ILocatorObject, new()
        {
            LocatoObjectImplementation<T>.Implementation = @object;
        }

        internal static T GetObject<T>()
            where T: ILocatorObject, new()
        {
            if (LocatoObjectImplementation<T>.Implementation == null)
                SetObject(new T());

            return LocatoObjectImplementation<T>.Implementation;
        }

        private static class LocatoObjectImplementation<T>
            where T : ILocatorObject
        {
            public static T Implementation;
        }
    }
}
