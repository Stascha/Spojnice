using System.ComponentModel.DataAnnotations;

namespace User.Models
{
    /// Klasa koja sadrzi informacije o igracu
    public class UserModel
    {
       
            /// ID primary key igraca - jedinstveni identifikator igraca
            [Key]
            public int Id { get; set; }
            /// Korisnicko ime igraca
            [Required]
            public string Username { get; set; }
            /// Sadrzi hesiranu lozinku igraca
            [Required]
            public string Password { get; set; }
            [Required]
            /// Sadrzi informaciju da li je igrac admin ili ne
            public string Role { get; set; }
            [Required]
            /// Boolean vrednost koja govori da li igrac zeli da dobija email notifikacije od aplikacije.
            public bool Notifications { get; set; }
            [Required]
            ///Slucajni string koji se koristi za identifikaciju korisnika dok mu se pruza mogucnost da omguci ili 
            ///onemoguci notifikacije od aplikacije.
            public string NotificationToken { get; set; }
            /// Email adresa
            [Required]
            public string Email { get; set; }
       

    }

}