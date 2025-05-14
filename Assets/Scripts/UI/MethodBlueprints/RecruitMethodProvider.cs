namespace UI.MethodBlueprints
{
    public interface RecruitMethodProvider
    {
        public void Recruit(int amount);
        public bool CheckIfRecruitmentPossible(int amount);
    }
}