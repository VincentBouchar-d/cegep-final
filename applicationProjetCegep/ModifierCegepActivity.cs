using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;
using ProjetCegep.DTOs;
using ProjetCegep.Controleurs;

namespace applicationProjetCegep
{
    [Activity(Label = "Activity1")]
    public class ModifierCegepActivity : AppCompatActivity
    {
        /// <summary>
        /// Label contenant le nom du cégep
        /// </summary>
        private TextView lblNomCegep;

        /// <summary>
        /// EditText contenant l'adresse du cégep
        /// </summary>
        private EditText edtAdresseCegep;
        /// <summary>
        /// EditText contenant la ville du cégep
        /// </summary>
        private EditText edtVilleCegep;
        /// <summary>
        /// EditText contenant la province du cégep
        /// </summary>
        private EditText edtProvinceCegep;
        /// <summary>
        /// EditText contenant le code postal du cégep
        /// </summary>
        private EditText edtCodePostalCegep;
        /// <summary>
        /// EditText contenant le numéro de téléphone du cégep
        /// </summary>
        private EditText edtTelephoneCegep;
        /// <summary>
        /// EditText contenant le courriel du cégep
        /// </summary>
        private EditText edtCourrielCegep;

        /// <summary>
        /// Bouton pour créer un Cegep
        /// </summary>
        private Button btnModifierCegep;
        /// <summary>
        /// Objet contenant les informations du Cegep
        /// </summary>
        private CegepDTO cegepDTO;

        /// <summary>
        /// Fonction OnCreate qui s'exécute lorsque l'activité se lance
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ModifierCegep);

            lblNomCegep = FindViewById<TextView>(Resource.Id.lblNomCegep);
            edtAdresseCegep = FindViewById<EditText>(Resource.Id.edtAdresseCegep);
            edtVilleCegep = FindViewById<EditText>(Resource.Id.edtVilleCegep);
            edtProvinceCegep = FindViewById<EditText>(Resource.Id.edtProvinceCegep);
            edtCodePostalCegep = FindViewById<EditText>(Resource.Id.edtCodePostalCegep);
            edtTelephoneCegep = FindViewById<EditText>(Resource.Id.edtTelephoneCegep);
            edtCourrielCegep = FindViewById<EditText>(Resource.Id.edtCourrielCegep);
            btnModifierCegep = FindViewById<Button>(Resource.Id.btnModifierCegep);
            cegepDTO = CegepControleur.Instance.ObtenirCegep(Intent.GetStringExtra("paramNomCegep"));
            
            // bouton qui permet de modifier un Cegep
            btnModifierCegep.Click += delegate
            {
                CegepControleur.Instance.ModifierCegep(new CegepDTO(lblNomCegep.Text, edtAdresseCegep.Text, edtVilleCegep.Text, edtProvinceCegep.Text, edtCodePostalCegep.Text, edtTelephoneCegep.Text, edtCourrielCegep.Text));
                Finish();

            };
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
        /// Fonction qui permet d'afficher les bonnes informations dans les EditTexts
        /// </summary>
        private void RafraichirDonnees()
        {
            
            
            lblNomCegep.Text = cegepDTO.Nom;
            edtAdresseCegep.Text = cegepDTO.Adresse;
            edtVilleCegep.Text = cegepDTO.Ville;
            edtProvinceCegep.Text = cegepDTO.Province;
            edtCodePostalCegep.Text = cegepDTO.CodePostal;
            edtTelephoneCegep.Text = cegepDTO.Telephone;
            edtCourrielCegep.Text = cegepDTO.Courriel;
        }

        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_modifierCegep, menu);
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