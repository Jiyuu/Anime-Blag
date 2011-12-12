using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facebook.Samples.AuthenticationTool
{
    public partial class MainForm : Form
    {
        private string _appId = "264818926868446";
        private string[] _extendedPermissions = new[] { "publish_stream", "manage_pages", "create_event", "manage_friendlists", "manage_notifications", "offline_access", "publish_checkins", "rsvp_event", "sms", "ads_management", "xmpp_login", "read_stream", "read_requests", "read_mailbox", "read_insights", "read_friendlists", "email", "user_work_history", "user_website", "user_videos", "user_status", "user_religion_politics", "user_relationship_details", "user_relationships", "user_photos", "user_photo_video_tags", "user_online_presence", "user_notes", "user_location", "user_likes", "user_interests", "user_hometown", "user_groups", "user_events", "user_education_history", "user_checkins", "user_birthday", "user_activities", "user_about_me", "friends_work_history", "friends_website", "friends_videos", "friends_status", "friends_religion_politics", "friends_relationship_details", "friends_relationships", "friends_photos", "friends_photo_video_tags", "friends_online_presence", "friends_notes", "friends_location", "friends_likes", "friends_interests", "friends_hometown", "friends_groups", "friends_events", "friends_education_history", "friends_checkins", "friends_birthday", "friends_activities", "friends_about_me" };

        public MainForm()
        {
            if (_appId == "{app id}")
            {
                throw new ApplicationException("Please set the _appId");
            }

            InitializeComponent();
        }

        private void btnFacebookLogin_Click(object sender, EventArgs e)
        {
            var facebookLoginDialog = new FacebookLoginDialog(_appId, _extendedPermissions);
            facebookLoginDialog.ShowDialog();

            DisplayAppropriateMessage(facebookLoginDialog.FacebookOAuthResult);
        }

        private void btnFacebookLoginDifferent_Click(object sender, EventArgs e)
        {
            var facebookLoginDialog = new FacebookLoginDialog(_appId, _extendedPermissions, true);
            facebookLoginDialog.ShowDialog();

            DisplayAppropriateMessage(facebookLoginDialog.FacebookOAuthResult);
        }

        private void DisplayAppropriateMessage(FacebookOAuthResult facebookOAuthResult)
        {
            if (facebookOAuthResult == null)
            {
                // most likely user closed the FacebookLoginDialog, so do nothing
                return;
            }

            if (facebookOAuthResult.IsSuccess)
            {
                // we got the access token
                var infoDialog = new Info(facebookOAuthResult.AccessToken);
                infoDialog.ShowDialog();
            }
            else
            {
                // for some reason we failed to get the access token.
                // most likely the user clicked don't allow
                MessageBox.Show(facebookOAuthResult.ErrorDescription);
            }
        }
    }
}