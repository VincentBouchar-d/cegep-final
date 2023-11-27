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
        private TextView lblNomCegep;
        private EditText edtAdresseCegep;
        private EditText edtVilleCegep;
        private EditText edtProvinceCegep;
        private EditText edtCodePostalCegep;
        private EditText edtTelephoneCegep;
        private EditText edtCourrielCegep;

        private Button btnModifierCegep;

        private CegepDTO cegepDTO;
        private CegepDTO cegepModif;

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
            
            
            btnModifierCegep.Click += delegate
            {
                CegepControleur.Instance.ModifierCegep(new CegepDTO(lblNomCegep.Text, edtAdresseCegep.Text, edtVilleCegep.Text, edtProvinceCegep.Text, edtCodePostalCegep.Text, edtTelephoneCegep.Text, edtCourrielCegep.Text));
                Finish();

            };
        }



        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }

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