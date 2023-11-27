using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace ProjetCegep.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class DialoguesUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public static void AfficherToasts(AppCompatActivity sender, string message)
        {
            Toast.MakeText(sender, message, ToastLength.Long).Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="titre"></param>
        /// <param name="message"></param>
        public static void AfficherMessageOK(AppCompatActivity sender, string titre, string message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(sender);
            builder.SetPositiveButton("OK", (send, args) => {});
            AlertDialog dialog = builder.Create();
            dialog.SetTitle(titre);
            dialog.SetMessage(message);
            dialog.Show();
        }
    }
}