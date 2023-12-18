using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using Android.Widget;
using Javax.Security.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Service.Voice.VoiceInteractionSession;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using applicationProjetCegep.Adapteurs;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using ProjetCegep.Utils;
using ProjetCegep.Modeles;

namespace applicationProjetCegep
{
    [Activity(Label = "AfficherCegep")]
    public class AfficherCegepActivity : AppCompatActivity
    {
        
        /// <summary>
        /// Liste contenant les cegepDTO
        /// </summary>
        private DepartementDTO[] listeDepartement;
        /// <summary>
        /// Adpateur pour la listeCegep
        /// </summary>
        private ListeDepartementAdapteur adapteurListeAfficherDepartement;
        /// <summary>
        /// Listview contenant les noms des cegeps
        /// </summary>
        private ListView listView;
        /// <summary>
        /// Label du nom du cegep
        /// </summary>
        private TextView lblNomCegep;
        /// <summary>
        /// Label de l'adresse du cegep
        /// </summary>
        private TextView lblAdresseCegep;
        /// <summary>
        /// Label de la ville du cegep
        /// </summary>
        private TextView lblVilleCegep;
        /// <summary>
        /// Label de la province du cegep
        /// </summary>
        private TextView lblProvinceCegep;
        /// <summary>
        /// Label du code postal du cegep
        /// </summary>
        private TextView lblCodePostalCegep;
        /// <summary>
        /// Label du telephone du cegep
        /// </summary>
        private TextView lblTelephoneCegep;
        /// <summary>
        /// Label du courriel du cegep
        /// </summary>
        private TextView lblCourrielCegep;
        /// <summary>
        /// Objet contenant les informations du cegep
        /// </summary>
        private CegepDTO cegepDTO;

        
        /// <summary>
        /// Champ d'édition contenant le nom du département à creer
        /// </summary>
        private EditText edtNomDepartement;
        /// <summary>
        /// Champ d'édition contenant le numéro du departement à créer
        /// </summary>
        private EditText edtNumeroDepartement;
        /// <summary>
        /// Champ d'édition contenant la description du département à créer
        /// </summary>
        private EditText edtDescriptionDepartement;
        /// <summary>
        /// Bouton pour créer le département
        /// </summary>
        private Button   btnAjouterDepartement;
        /// <summary>
        /// Fonction OnCreate qui se lance lors du début de l'activité
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AfficherCegep);

            // Liens entre les variables et les objets dans le layout
            listView = FindViewById<ListView>(Resource.Id.listViewAfficher);
            lblNomCegep = FindViewById<TextView>(Resource.Id.lblNomCegep);
            lblAdresseCegep = FindViewById<TextView>(Resource.Id.lblAdresseCegep);
            lblVilleCegep = FindViewById<TextView>(Resource.Id.lblVilleCegep);
            lblProvinceCegep = FindViewById<TextView>(Resource.Id.lblProvinceCegep);
            lblCodePostalCegep = FindViewById<TextView>(Resource.Id.lblCodePostalCegep);
            lblTelephoneCegep = FindViewById<TextView>(Resource.Id.lblTelephoneCegep);
            lblCourrielCegep = FindViewById<TextView>(Resource.Id.lblCourrielCegep);

            
            edtNomDepartement = FindViewById<EditText>(Resource.Id.edtNomDepartement);
            edtNumeroDepartement = FindViewById<EditText>(Resource.Id.edtNumeroDepartement);
            edtDescriptionDepartement= FindViewById<EditText>(Resource.Id.edtDescriptionDepartement);
            btnAjouterDepartement = FindViewById<Button>(Resource.Id.btnAjouterDepartement);

            // Bouton permettant d'ajouter un département
            btnAjouterDepartement.Click += delegate
            {
                if ((edtDescriptionDepartement.Text.Length > 0) && (edtNumeroDepartement.Text.Length > 0) && (edtNomDepartement.Text.Length > 0))
                {
                    try
                    {
                        string nom = edtNomDepartement.Text;
                        CegepControleur.Instance.AjouterDepartement(Intent.GetStringExtra("paramNomCegep"),new DepartementDTO(edtNumeroDepartement.Text, edtNomDepartement.Text, edtDescriptionDepartement.Text));
                        RafraichirDonnees();
                        DialoguesUtils.AfficherToasts(this, nom + " ajouté !!!");
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                    }
                }
                else
                    DialoguesUtils.AfficherMessageOK(this, "Erreur", "Veuillez remplir tous les champs...");
            };
            // Permet de lancer l'activité AfficherDepartementActivity pour le département du listView cliqué.
            listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteDepartementDetails = new Intent(this, typeof(AfficherDepartementActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteDepartementDetails.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                activiteDepartementDetails.PutExtra("paramNomDepartement", listeDepartement[e.Position].Nom);
                //On démarre la nouvelle activité.
                StartActivity(activiteDepartementDetails);
            };
        }
        /// <summary>
        /// Fonction lancée lorsque l'activité recommence
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }
        /// <summary>
        /// fonction qui permet d'afficher les bonnes informations dans les labels
        /// </summary>
        private void RafraichirDonnees()
        {
            listeDepartement = CegepControleur.Instance.ObtenirListeDepartement(Intent.GetStringExtra("paramNomCegep")).ToArray();
            adapteurListeAfficherDepartement = new ListeDepartementAdapteur(this, listeDepartement);
            listView.Adapter = adapteurListeAfficherDepartement;
            cegepDTO = CegepControleur.Instance.ObtenirCegep(Intent.GetStringExtra("paramNomCegep"));

            lblNomCegep.Text = cegepDTO.Nom;
            lblAdresseCegep.Text = cegepDTO.Adresse;
            lblVilleCegep.Text = cegepDTO.Ville;
            lblProvinceCegep.Text = cegepDTO.Province;
            lblCodePostalCegep.Text = cegepDTO.CodePostal;
            lblTelephoneCegep.Text = cegepDTO.Telephone;
            lblCourrielCegep.Text = cegepDTO.Courriel;
        }

        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_afficherCegep, menu);
            return base.OnCreateOptionsMenu(menu);

        }






        /// <summary>/// Évenement exécuté lors d'un choix dans le menu./// </summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuModifier:
                    var ModifierCegepActivity = new Intent(this, typeof(ModifierCegepActivity));
                    ModifierCegepActivity.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                    StartActivity(ModifierCegepActivity);
                    break;
                case Resource.Id.menuSupprimer:
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetPositiveButton("NON", (sender, args) => { Finish(); });
                    builder.SetNegativeButton("OUI", (sender, args) => {
                        foreach (DepartementDTO departementDTO in CegepControleur.Instance.ObtenirListeDepartement(Intent.GetStringExtra("paramNomCegep")))
                        {
                            foreach (EnseignantDTO enseignantDTO in CegepControleur.Instance.ObtenirListeEnseignant(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom)) 
                            {
                                CegepControleur.Instance.SupprimerEnseignant(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom, enseignantDTO.NoEmploye);
                            }
                            foreach (CoursDTO coursDTO in CegepControleur.Instance.ObtenirListeCours(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom))
                            {
                                CegepControleur.Instance.SupprimerCours(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom, coursDTO.Nom);
                            }
                            CegepControleur.Instance.SupprimerDepartement(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom);
                        }
                        CegepControleur.Instance.SupprimerCegep(Intent.GetStringExtra("paramNomCegep"));
                        Finish();
                    });
                    AlertDialog dialog = builder.Create();
                    dialog.SetTitle("*** ATTENTION  ***");
                    dialog.SetMessage("Voulez-vous VRAIMENT supprimer ce Cégep ? Cette action est irréversible.");
                    dialog.Window.SetGravity(GravityFlags.Center);
                    dialog.Show();
                    
                    break;
                case Resource.Id.menuRetour:
                    Finish();
                    break;
                case Resource.Id.menuQuitter:
                    FinishAffinity();
                    break;
                case Resource.Id.menuVider:

                    CegepControleur.Instance.ViderDepartement(Intent.GetStringExtra("paramNomCegep"));
                    
                    RafraichirDonnees();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        
    }
}