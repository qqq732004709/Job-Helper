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

                //实例化StringBuilder类，获取文本
                StringBuilder content = new StringBuilder();
                content.Append(document.Pages[0].ExtractText());
                pdfContent = content.ToString();

                loadedPdf = true;
                MessageBox.Show("PDF内容加载完成");

            }
            catch (Exception ex)
            {
                MessageBox.Show("遇到问题：e" + ex.Message);
            }
        }
    }

    private void SaveKey_Click(object sender, EventArgs e)
    {
        key = ApiKey.Text;
        MessageBox.Show("API Key保存成功");
    }

    private async void FetchResume_Click(object sender, EventArgs e)
    {
        if (!loadedPdf
            || string.IsNullOrWhiteSpace(pdfContent)
            || string.IsNullOrWhiteSpace(key)
            || string.IsNullOrEmpty(JobDescription.Text))
        {
            MessageBox.Show("请检查是否上传PDF或填写OpenAPI Key以及岗位要求");
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
                    ChatMessage.FromSystem(@"你是一位精通简历修改与润色的专家。我会提供给你我将应聘的岗位要求与我的简历内容。请你修改我的简历内容，以下是修改的要求：
                        1.简历内容要根据我提供的简历内容进行修改
                        2.将简历内容根据岗位要求进行修改成和岗位要求匹配
                        3.只返回markdown格式的简历内容，不需要加其他说明文字"),
                    ChatMessage.FromUser($"我将应聘的岗位要求是：\n\n{JobDescription.Text} \n\n 我的简历内容是：\n\n {pdfContent} \n\n"),
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
                MessageBox.Show($"GPT未成功返回结果：{completionResult.HttpStatusCode}");
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show("遇到问题：e" + ex.Message);
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

        // 显示消息框提示用户已复制到剪贴板
        MessageBox.Show("文本已复制到剪贴板！");
    }
}
