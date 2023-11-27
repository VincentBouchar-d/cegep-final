using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using applicationProjetCegep.Adapteurs;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using ProjetCegep.Utils;
using System;
using static Android.Service.Voice.VoiceInteractionSession;

namespace applicationProjetCegep
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class CreerCegepActivity : AppCompatActivity
    {
        /// <summary>
        /// bouton pour ajouter un cégep
        /// </summary>
        private Button btnAjouterCegep;
        /// <summary>
        /// Liste contenant les cegepDTO
        /// </summary>
        private CegepDTO[] listeCegep;
        /// <summary>
        /// Adpateur pour la listeCegep
        /// </summary>
        private ListeCegepAdapteur adapteurListeCegep;

        private ListView listeVueCegep;
        private EditText edtNomCegep;
        private EditText edtAdresseCegep;
        private EditText edtVilleCegep;
        private EditText edtProvinceCegep;
        private EditText edtCodePostalCegep;
        private EditText edtTelephoneCegep;
        private EditText edtCourrielCegep;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AjouterCegep);


            btnAjouterCegep = FindViewById<Button>(Resource.Id.btnAjouterCegep);
            listeVueCegep = FindViewById<ListView>(Resource.Id.listViewCegep);
            edtNomCegep = FindViewById<EditText>(Resource.Id.edtNomCegep);
            edtAdresseCegep = FindViewById<EditText>(Resource.Id.edtAdresseCegep);
            edtVilleCegep = FindViewById<EditText>(Resource.Id.edtVilleCegep);
            edtProvinceCegep = FindViewById<EditText>(Resource.Id.edtProvinceCegep);
            edtCodePostalCegep = FindViewById<EditText>(Resource.Id.edtCodePostalCegep);
            edtTelephoneCegep = FindViewById<EditText>(Resource.Id.edtTelephoneCegep);
            edtCourrielCegep = FindViewById<EditText>(Resource.Id.edtCourrielCegep);



            btnAjouterCegep.Click += delegate
            {
                if ((edtAdresseCegep.Text.Length > 0) && (edtVilleCegep.Text.Length > 0) && (edtProvinceCegep.Text.Length > 0) && (edtCodePostalCegep.Text.Length > 0) && (edtTelephoneCegep.Text.Length > 0) && (edtCourrielCegep.Text.Length > 0))
                {
                    try
                    {
                        string nom = edtNomCegep.Text;
                        CegepControleur.Instance.AjouterCegep(new CegepDTO(edtNomCegep.Text, edtAdresseCegep.Text, edtVilleCegep.Text, edtProvinceCegep.Text, edtCodePostalCegep.Text, edtTelephoneCegep.Text, edtCourrielCegep.Text));
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

            listeVueCegep.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent activiteCegepDetails = new Intent(this, typeof(AfficherCegepActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteCegepDetails.PutExtra("paramNomCegep", listeCegep[e.Position].Nom);
                //On démarre la nouvelle activité.
                StartActivity(activiteCegepDetails);
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }

        private void RafraichirDonnees()
        {
            listeCegep = CegepControleur.Instance.ObtenirListeCegep().ToArray();
            adapteurListeCegep = new ListeCegepAdapteur(this, listeCegep);
            listeVueCegep.Adapter = adapteurListeCegep;
        }


        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_creerCegep, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        /// <summary>/// Évenement exécuté lors d'un choix dans le menu./// </summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Quitter:
                    Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}