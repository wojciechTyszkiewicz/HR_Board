namespace HR_Board.Config
{
    public class JwtTokenSettings
    {

        public string SymmetricSecurityKey { get; set; } = null!;
        public string JwtRegisteredClaimNamesSub { get; set; } = null!;

    }
}
