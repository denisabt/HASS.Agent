﻿using Syncfusion.Windows.Forms;
using HASS.Agent.Functions;

namespace HASS.Agent.Forms
{
    public partial class Help : MetroForm
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            // catch all key presses
            KeyPreview = true;

            // set version
            var beta = Variables.Beta ? " [BETA]" : string.Empty;
            LblVersion.Text = $"{Variables.Version}{beta}";
        }

        private void PbHassAgentLogo_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://github.com/hass-agent/HASS.Agent");

        private void BtnClose_Click(object sender, EventArgs e) => Close();

        private async void LblAbout_Click(object sender, EventArgs e)
        {
            if (await HelperFunctions.TryBringToFront("About")) return;

            var form = new About();
            form.FormClosed += delegate { form.Dispose(); };
            form.Show(this);
        }

        private void Help_ResizeEnd(object sender, EventArgs e)
        {
            if (Variables.ShuttingDown) return;
            if (!IsHandleCreated) return;
            if (IsDisposed) return;

            try
            {
                Refresh();
            }
            catch
            {
                // best effort
            }
        }

        private void PbGitHub_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://github.com/hass-agent/HASS.Agent");

        private void LblGitHub_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://github.com/hass-agent/HASS.Agent");

        private void LblGitHubInfo_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://github.com/hass-agent/HASS.Agent");

        private void PbDiscord_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://discord.com/invite/JfZj98xqJr");

        private void LblDiscord_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://discord.com/invite/JfZj98xqJr");

        private void LblDiscordInfo_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://discord.com/invite/JfZj98xqJr");

        private void PbHAForum_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://community.home-assistant.io/t/hass-agent-a-new-windows-based-client-to-receive-notifications-perform-quick-actions-and-much-more/369094");

        private void LblHAForum_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://community.home-assistant.io/t/hass-agent-a-new-windows-based-client-to-receive-notifications-perform-quick-actions-and-much-more/369094");

        private void LblHAInfo_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://community.home-assistant.io/t/hass-agent-a-new-windows-based-client-to-receive-notifications-perform-quick-actions-and-much-more/369094");

        private void PbDocumentation_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://www.hass-agent.io");

        private void LblDocumentation_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://www.hass-agent.io");

        private void LblDocumentationInfo_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://www.hass-agent.io");

        private void Help_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape) return;
            Close();
        }
    }
}
