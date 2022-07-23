using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;




namespace AgeCalculator.Pages
{

   

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;



        public String message { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public static SqlConnection con = new SqlConnection(@"Data Source=M\SQLEXPRESS;Initial Catalog=AgeCalculator;Integrated Security=True");



        [BindProperty]
            public String ad { get; set; }

            [BindProperty]
            public String soyad { get; set; }

            [BindProperty]
            public DateTime dogumTarihi { get; set; } = new DateTime(1950, 01, 01);

        [BindProperty]
        public String cinsiyet { get; set; }

        [BindProperty]
        public String sehir { get; set; }





        


        public void OnGet()
        {

        }
        public void OnPost()
        {
            
            var bugun = DateTime.Now;
            var yas = bugun.Year - dogumTarihi.Year;

            if (bugun.Month < dogumTarihi.Month)
            {
                yas--;
            }

            if (bugun.Month == dogumTarihi.Month && bugun.Day < dogumTarihi.Day)
            {
                yas--;
            }

            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Tbl_Person (PersonName,PersonSurname,PersonAge,PersonGender,PersonCity) VALUES (@PersonName,@PersonSurname,@PersonAge,@PersonGender,@PersonCity)", con);
            cmd.Parameters.AddWithValue("@PersonName", ad);
            cmd.Parameters.AddWithValue("@PersonSurname", soyad);
            cmd.Parameters.AddWithValue("@PersonAge", yas);
            cmd.Parameters.AddWithValue("@PersonGender", cinsiyet); 
            cmd.Parameters.AddWithValue("@PersonCity", sehir);

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();

           
            message = String.Format("Hoş geldin {0} {1} yaşınız= {2} , başarıyla kayıt edildi.", ad, soyad, yas);
        }


    }

}
