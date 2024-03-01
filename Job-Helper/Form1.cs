using OpenAI.Managers;
using OpenAI;
using Spire.Pdf;
using System.Text;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using SplashScreen.WindowsForms;

namespace Job_Helper;

public partial class MainForm : Form
{
    private bool loadedPdf = false;
    private string pdfContent = string.Empty;
    private string key = string.Empty;

    public MainForm()
    {
        InitializeComponent();
    }

    private void ChooseResumeDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }

    private void SelectFile_Click(object sender, EventArgs e)
    {
        if (ChooseResumeDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                loadedPdf = false;
                FilePath.Text = ChooseResumeDialog.FileName;

                PdfDocument document = new();
                document.LoadFromFile(ChooseResumeDialog.FileName);

                //ʵ����StringBuilder�࣬��ȡ�ı�
                StringBuilder content = new StringBuilder();
                content.Append(document.Pages[0].ExtractText());
                pdfContent = content.ToString();

                loadedPdf = true;
                MessageBox.Show("PDF���ݼ������");

            }
            catch (Exception ex)
            {
                MessageBox.Show("�������⣺e" + ex.Message);
            }
        }
    }

    private void SaveKey_Click(object sender, EventArgs e)
    {
        key = ApiKey.Text;
        MessageBox.Show("API Key����ɹ�");
    }

    private async void FetchResume_Click(object sender, EventArgs e)
    {
        if (!loadedPdf
            || string.IsNullOrWhiteSpace(pdfContent)
            || string.IsNullOrWhiteSpace(key)
            || string.IsNullOrEmpty(JobDescription.Text))
        {
            MessageBox.Show("�����Ƿ��ϴ�PDF����дOpenAPI Key�Լ���λҪ��");
            return;
        }

        Hide();
        var splasher = new Splasher("Generating Resume...", "teched by CXY");
        splasher.Show();

        try
        {
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = key
            });

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(@"����һλ��ͨ�����޸�����ɫ��ר�ҡ��һ��ṩ�����ҽ�ӦƸ�ĸ�λҪ�����ҵļ������ݡ������޸��ҵļ������ݣ��������޸ĵ�Ҫ��
                        1.��������Ҫ�������ṩ�ļ������ݽ����޸�
                        2.���������ݸ��ݸ�λҪ������޸ĳɺ͸�λҪ��ƥ��
                        3.ֻ����markdown��ʽ�ļ������ݣ�����Ҫ������˵������"),
                    ChatMessage.FromUser($"�ҽ�ӦƸ�ĸ�λҪ���ǣ�\n\n{JobDescription.Text} \n\n �ҵļ��������ǣ�\n\n {pdfContent} \n\n"),
                },
                Model = Models.Gpt_3_5_Turbo,
            });

            splasher.Close();
            Show();
            Activate();

            if (completionResult.Successful)
            {
                AiResume.Text = completionResult.Choices.First().Message.Content;
            }
            else
            {
                MessageBox.Show($"GPTδ�ɹ����ؽ����{completionResult.HttpStatusCode}");
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show("�������⣺e" + ex.Message);
        }
        finally
        {
            splasher.Close();
            Show();
            Activate();
        }
    }

    private void CopyResume_Click(object sender, EventArgs e)
    {
        Clipboard.SetText(AiResume.Text);

        // ��ʾ��Ϣ����ʾ�û��Ѹ��Ƶ�������
        MessageBox.Show("�ı��Ѹ��Ƶ������壡");
    }
}
