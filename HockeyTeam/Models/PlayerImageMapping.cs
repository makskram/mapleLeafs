namespace HockeyTeam.Models
{
    public class PlayerImageMapping
    {
        public int ID { get; set; }
        public int ImageNumber { get; set; }
        public int PlayerID { get; set; }
        public int PlayerImageID { get; set; }
        public virtual Player Player { get; set; }
        public virtual PlayerImage PlayerImage { get; set; }
    }
}