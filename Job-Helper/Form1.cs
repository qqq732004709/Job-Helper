using OpenAI.Managers;
using OpenAI;
using Spire.Pdf;
using System.Text;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;

namespace Job_Helper;

public partial class Form1 : Form
{
    private bool loadedPdf = false;
    private string pdfContent = string.Empty;
    private string key = string.Empty;


    public Form1()
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
    }

    private void FilePath_Click(object sender, EventArgs e)
    {

    }

    private async void FetchResume_Click(object sender, EventArgs e)
    {
        if (!loadedPdf
            || string.IsNullOrWhiteSpace(pdfContent)
            || string.IsNullOrWhiteSpace(key)
            || string.IsNullOrEmpty(JobDescription.Text))
        {
            MessageBox.Show("请检查是否上传PDF或填写OpenAPI Key以及岗位要求");
        }

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
                    ChatMessage.FromSystem(@"你是一位精通简历修改与润色的专家。我会提供给你我将应聘的岗位要求与我的简历内容。请你帮我将简历内容进行润色与修改，以下是修改的要求：
                        1.简历内容要根据我提供的简历内容进行修改
                        2.简历内容要与目标求职岗位的岗位要求相匹配
                        3.只返回markdown格式的简历内容，不需要加其他说明文字"),
                    ChatMessage.FromUser($"我将应聘的岗位要求是：\n\n{JobDescription.Text} \n\n 我的简历内容是：\n\n {pdfContent} \n\n"),
                },
                Model = Models.Gpt_3_5_Turbo,
            });

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
    }
}
