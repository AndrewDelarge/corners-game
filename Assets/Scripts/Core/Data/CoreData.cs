namespace Core.Data
{
    public static class CoreData
    {
        public enum SceneIndexes
        {
            ROOT = 0,
            MENU = 1,
            GAME = 2,
            EMPTY = 3,
        }
        
        public static GameSettings GameSettings;

        public static BaseScenes BaseScenes;
    }
}