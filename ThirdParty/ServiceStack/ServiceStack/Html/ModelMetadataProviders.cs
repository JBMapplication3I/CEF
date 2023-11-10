
namespace ServiceStack.Html
{
    public class ModelMetadataProviders
    {
        private ModelMetadataProvider currentProvider;
        private static readonly ModelMetadataProviders Instance = new();

        internal ModelMetadataProviders()
        {
            currentProvider = new PocoMetadataProvider();
        }

        public static ModelMetadataProvider Current
        {
            get => Instance.CurrentInternal;
            set => Instance.CurrentInternal = value;
        }

        internal ModelMetadataProvider CurrentInternal
        {
            get => currentProvider;
            set => currentProvider = value ?? new EmptyModelMetadataProvider();
        }
    }

}