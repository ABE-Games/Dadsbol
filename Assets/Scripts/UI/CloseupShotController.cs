using Core;
using Model;
using UnityEngine;

public class CloseupShotController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject[] cameras;

    private readonly ABEModel model = Simulation.GetModel<ABEModel>();

    public void PlayNext()
    {
        cameras[1].SetActive(true);
        cameras[0].SetActive(false);
    }

    public void End()
    {
        mainCamera.SetActive(true);
        cameras[0].SetActive(false);
        cameras[1].SetActive(false);

        model.gameController.finishedTeamCloseup = true;
    }
}
