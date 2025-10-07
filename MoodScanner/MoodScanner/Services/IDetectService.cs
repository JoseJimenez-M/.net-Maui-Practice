using System;
using MoodScanner.Models;

namespace MoodScanner.Services
{
    public interface IDetectService
    {
        DetectResult Detect(byte[] file);
    }
}