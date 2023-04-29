using Game.App;

namespace Lessons.Meta
{
    public sealed class QuestRepository : DataRepository<QuestData>
    {
        protected override string Key => "QuestData";

        public bool LoadQuest(out QuestData data)
        {
            return this.LoadData(out data);
        }

        public void SaveQuest(QuestData data)
        {
            this.SaveData(data);
        }

        public void RemoveQuest()
        {
           this.RemoveData();
        }
    }
}