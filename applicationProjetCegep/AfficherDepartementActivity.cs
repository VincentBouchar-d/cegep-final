using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Android.Widget;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;

namespace applicationProjetCegep
{
    [Activity(Label = "AfficherDepartement")]
    public class AfficherDepartementActivity : AppCompatActivity
    {
        
        
        /// <summary>
        /// Label contenant le nom du département
        /// </summary>
        private TextView lblNomDepartement;
        /// <summary>
        /// Label contenant le numéro du département
        /// </summary>
        private TextView lblNumeroDepartement;
        /// <summary>
        /// Label contenant la description du département
        /// </summary>
        private TextView lblDescriptionDepartement;
        /// <summary>
        /// Objet contenant les informations du département
        /// </summary>
        private DepartementDTO departementDTO;

        
        
        
        /// <summary>
        /// Fonction OnCreate qui s'exécute lors du lancement de l'activité
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AfficherDepartement);


            
            lblNomDepartement = FindViewById<TextView>(Resource.Id.lblNomDepartement);
            lblNumeroDepartement = FindViewById<TextView>(Resource.Id.lblNumeroDepartement);
            lblDescriptionDepartement = FindViewById<TextView>(Resource.Id.lblDescriptionDepartement);






            RafraichirDonnees();
        }
        /// <summary>
        /// Fonction OnResume qui s'exécute lorsque l'activité recommence
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }
        /// <summary>
        /// Fonction qui permet d'afficher les bonnes informations dans les labels
        /// </summary>
        private void RafraichirDonnees()
        {
            
            departementDTO = CegepControleur.Instance.ObtenirDepartement(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"));

            lblNomDepartement.Text = departementDTO.Nom;
            lblNumeroDepartement.Text = departementDTO.No;
            lblDescriptionDepartement.Text = departementDTO.Description;
     
        }

        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_afficherDepartement, menu);
            return base.OnCreateOptionsMenu(menu);

        }

        




        /// <summary>/// Évenement exécuté lors d'un choix dans le menu./// </summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuSupprimer:
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetPositiveButton("NON", (sender, args) => { Finish(); });
                    builder.SetNegativeButton("OUI", (sender, args) => {
                        foreach (EnseignantDTO enseignantDTO in CegepControleur.Instance.ObtenirListeEnseignant(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom))
                        {
                            CegepControleur.Instance.SupprimerEnseignant(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom, enseignantDTO.NoEmploye);
                        }
                        foreach (CoursDTO coursDTO in CegepControleur.Instance.ObtenirListeCours(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom))
                        {
                            CegepControleur.Instance.SupprimerCours(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom, coursDTO.Nom);
                        }
                        CegepControleur.Instance.SupprimerDepartement(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom);
                            Finish();
                    });
                    AlertDialog dialog = builder.Create();
                    dialog.SetTitle("*** ATTENTION  ***");
                    dialog.SetMessage("Voulez-vous VRAIMENT supprimer ce département ? Cette action est irréversible.");
                    dialog.Window.SetGravity(GravityFlags.Center);
                    dialog.Show();
                    
                    break;
                case Resource.Id.menuModifier:
                    var ModifierDepartementActivity = new Intent(this, typeof(ModifierDepartementActivity));
                    ModifierDepartementActivity.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                    ModifierDepartementActivity.PutExtra("paramNomDepartement", departementDTO.Nom);
                    StartActivity(ModifierDepartementActivity);
                    break;  
                case Resource.Id.menuRetour:
                    Finish();
                    break;
                case Resource.Id.menuCreerEnseignant:
                    var CreerEnseignantActivity = new Intent(this, typeof(CreerEnseignantActivity));
                    CreerEnseignantActivity.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                    CreerEnseignantActivity.PutExtra("paramNomDepartement", departementDTO.Nom);
                    StartActivity(CreerEnseignantActivity);
                    break;
                case Resource.Id.menuCreerCours:
                    var CreerCoursActivity = new Intent(this, typeof(CreerCoursActivity));
                    CreerCoursActivity.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                    CreerCoursActivity.PutExtra("paramNomDepartement", departementDTO.Nom);
                    StartActivity(CreerCoursActivity);
                    break;
                case Resource.Id.menuQuitter:
                    FinishAffinity();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        
        
    }
}