using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using UnityEngine;

public class RunScript : MonoBehaviour
{
    private void Start()
    {
        RunModel();
    }

    public void RunModel()
    {
        var psi = new ProcessStartInfo();

        var script = Application.dataPath + "/Scripts/Python/shopping/RL.py";
        var argument = "test";
        
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;
        psi.Arguments = "python3 " + script + " " + argument;
        psi.FileName = script;
        UnityEngine.Debug.Log(psi.Arguments);
        var errors = "";
        var results = "";

        using(var process = Process.Start(psi))
        {
            errors = process.StandardError.ReadToEnd();
            results = process.StandardOutput.ReadToEnd();

            UnityEngine.Debug.Log("Errors:");
            UnityEngine.Debug.Log(errors);
            UnityEngine.Debug.Log("Results");
            UnityEngine.Debug.Log(results);
        }
    }
}
