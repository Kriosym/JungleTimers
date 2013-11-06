namespace JungleTimers
{
    class CustomButton: System.Windows.Forms.Button
    {
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}
