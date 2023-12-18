using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using ProjetCegep.DTOs;
using ProjetCegep.Controleurs;

namespace applicationProjetCegep
{
    [Activity(Label = "Modifier département")]
    public class ModifierDepartementActivity : AppCompatActivity
    {   
        /// <summary>
        /// TextView contenant le nom du cours
        /// </summary>
        private TextView edtNomModifier;
        /// <summary>
        /// EditText contenant le numéro du cours
        /// </summary>
        private EditText edtNoModifier;
        /// <summary>
        /// EditText contenant la description du cours
        /// </summary>
        private EditText edtDescriptionModifier;
        
        /// <summary>
        /// Bouton qui permet de modifier le cours
        /// </summary>
        private Button btnModifier;
        /// <summary>
        /// Objet contenant les informations du cours
        /// </summary>
        private DepartementDTO departementDTO;
        /// <summary>
        /// Variable contenant le nom du cégep
        /// </summary>
        private string paramNomCegep;
        /// <summary>
        /// Variable contenant le nom du département
        /// </summary>
        private string paramNomDepartement;


        /// <summary>
        /// Fonction OnCreate qui s'exécute lorsque l'activité se lance
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ModifierDepartement);

            edtNomModifier = FindViewById<TextView>(Resource.Id.edtNomModifier);
            edtNoModifier = FindViewById<EditText>(Resource.Id.edtNoModifier);
            edtDescriptionModifier = FindViewById<EditText>(Resource.Id.edtDescriptionModifier);

            paramNomCegep = Intent.GetStringExtra("paramNomCegep");
            paramNomDepartement = Intent.GetStringExtra("paramNomDepartement");
           
            btnModifier = FindViewById<Button>(Resource.Id.btnModifier);
            departementDTO = CegepControleur.Instance.ObtenirDepartement(paramNomCegep, paramNomDepartement);
            
            // Bouton qui permet de modifier le cours
            btnModifier.Click += delegate
            {
                CegepControleur.Instance.ModifierDepartement(paramNomCegep, paramNomDepartement, new DepartementDTO(edtNoModifier.Text, edtNomModifier.Text, edtDescriptionModifier.Text));
                Finish();

            };
        }
        
        /// <summary>
        /// Fonciton OnResume qui s'exécute lorsque l'activité recommence
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }
        /// <summary>
        /// Fonction qui permer d'afficher les bonnes informations dans les editText
        /// </summary>
        private void RafraichirDonnees()
        {
            edtNomModifier.Text = departementDTO.Nom;
            edtNoModifier.Text = departementDTO.No;
            edtDescriptionModifier.Text = departementDTO.Description;   
        }

        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_modifierDepartement, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>/// Évenement exécuté lors d'un choix dans le menu./// </summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuRetour:
                    Finish();
                    break;
                case Resource.Id.menuQuitter:
                    FinishAffinity();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}