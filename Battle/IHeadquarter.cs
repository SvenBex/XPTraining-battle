namespace Battle
{
    public interface IHeadquarter
    {
        int ReportEnlistment(string soldierName);
        void ReportCasualty(int soldierId);
        void ReportVictory(int remainingNumberOfSoldiers);
    }
}