using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace Notes
{
    public partial class FrmMain : Form
    {
        private const char SPLIT_CHAR = ':';
        Biz biz;
        public FrmMain()
        {
            InitializeComponent();
            biz = new Biz();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            lstTitles.Items.Clear();
            txtContent.Text = string.Empty;
            var lstData = biz.GetAllData();
            if (lstData != null && lstData.Count > 0)
            {
                foreach (var item in lstData)
                {
                    lstTitles.Items.Add(item.ID + SPLIT_CHAR.ToString() + item.Title);
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strTitle = Interaction.InputBox("请输入记事标题", "系统提示");
            if (string.IsNullOrEmpty(strTitle))
            {
                MessageBox.Show("记事本标题不能为空.");
                return;
            }
            string strContent = Interaction.InputBox("请输入记事本内容", "系统提示");
            NotesModel model = new NotesModel();
            model.Title = strTitle;
            model.Content = strContent;
            biz.InsertData(model);
            MessageBox.Show("记事添加成功.");
            LoadData();
        }

        private void lstTitles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strID = GetSelectID(false);
            if (string.IsNullOrEmpty(strID))
            {
                return;
            }

            var model = biz.GetModelDataByID(int.Parse(strID));
            if (model != null)
            {
                txtContent.Text = model.Content;
            }
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string strID = GetSelectID(true);
            if (string.IsNullOrEmpty(strID))
            {
                return;
            }
            var model = biz.GetModelDataByID(int.Parse(strID));
            if (model != null)
            {
                string strTitle = Interaction.InputBox("请输入记事标题", "系统提示");
                if (string.IsNullOrEmpty(strTitle))
                {
                    MessageBox.Show("记事本标题不能为空.");
                    return;
                }
                string strContent = Interaction.InputBox("请输入记事本内容", "系统提示");
                NotesModel newModel = new NotesModel();
                newModel.Title = strTitle;
                newModel.Content = strContent;
                newModel.ID = int.Parse(strID);
                biz.UpdateData(newModel);

                MessageBox.Show("记事修改成功.");
                LoadData();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strID = GetSelectID(true);
            if (string.IsNullOrEmpty(strID))
            {
                return;
            }

            if (DialogResult.OK == MessageBox.Show("确定要删除该记事吗？", "系统提示", MessageBoxButtons.OKCancel))
            {
                biz.DeleteData(int.Parse(strID));

                MessageBox.Show("记事删除成功.");
                LoadData();
            }

        }

        private string GetSelectID(Boolean boolNeedPop)
        {
            string strID = string.Empty;
            if (lstTitles.SelectedItem == null)
            {
                if (boolNeedPop)
                {
                    MessageBox.Show("请选择需要删除的记事");
                }
                return strID;
            }

            var strSelectText = lstTitles.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(strSelectText))
            {
                strID = strSelectText.Split(SPLIT_CHAR)[0];

            }
            return strID;
        }
    }
}
