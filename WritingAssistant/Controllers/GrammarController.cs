using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace WritingAssistant.Controllers
{
    public class GrammarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckGrammar(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                ViewBag.Error = "Vui lòng nhập văn bản.";
                return View("Index");
            }

            try
            {
                // Đường dẫn tới script Python
                var pythonPath = "D:/CheckGrammaNLP/env/Scripts/python.exe "; // Thay bằng đường dẫn đầy đủ nếu cần, ví dụ: "C:/Python39/python.exe"
                var scriptPath = "d:/CheckGrammaNLP/grammarVietNamese.py"; // Đường dẫn tới file Python

                // Tạo process để gọi script Python
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\" \"{inputText}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(processStartInfo);
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    ViewBag.Error = "Lỗi khi kiểm tra ngữ pháp: " + error;
                    return View("Index");
                }

                // Parse kết quả JSON
                var result = JsonSerializer.Deserialize<Dictionary<string, string>>(output);
                ViewBag.InputText = inputText;
                ViewBag.CorrectedText = result["corrected_text"];
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
            }

            return View("Index");
        }
    }
}