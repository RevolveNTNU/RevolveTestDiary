using RevolveTestDiaryXf.Interfaces;

namespace RevolveTestDiaryXf.Models
{
    public class DiaryGoal : IDiaryGoal
    {
        public bool Achieved { get; set; }

        public string Goal { get; set; }

        public DiaryGoal(string goal)
        {
            Goal = goal;
            Achieved = false;
        }
    }
}
