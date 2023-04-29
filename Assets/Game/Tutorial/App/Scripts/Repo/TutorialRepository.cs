using Game.App;

namespace Game.Tutorial.App
{
    public sealed class TutorialRepository : DataRepository<TutorialData>
    {
        protected override string Key => "TutorialData";

        public void SaveState(TutorialData data)
        {
            this.SaveData(data);
        }

        public bool LoadState(out TutorialData data)
        {
            return this.LoadData(out data);
        }
    }
}