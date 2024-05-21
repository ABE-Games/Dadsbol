using Core;
using Model;
using UnityEngine;

namespace Gameplay
{
    public class PlayerStaminaBar : Simulation.Event<PlayerThrow>
    {
        public float staminaBarFadeInDelay;

        private readonly ABEModel model = Simulation.GetModel<ABEModel>();

        public override void Execute()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && model.player.staminaSlider.value > 0)
            {
                model.player.depleted = false;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || model.player.staminaSlider.value <= 0)
            {
                model.player.depleted = true;
            }

            StaminaBarOpacityHandler();
        }

        private void StaminaBarOpacityHandler()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                model.player.staminaSlider.value -= Time.deltaTime * model.player.decreaseSpeed;
                model.player.staminaSlider.value = Mathf.Clamp(model.player.staminaSlider.value, 0f, 100f);

                // handle the opacity of the stamina bar
                model.player.background.opacity += Time.deltaTime * staminaBarFadeInDelay;
                model.player.fill.opacity = model.player.background.opacity;
            }
            else
            {
                model.player.staminaSlider.value += Time.deltaTime * model.player.recoverySpeed;
                model.player.staminaSlider.value = Mathf.Clamp(model.player.staminaSlider.value, 0f, 100f);

                // hide the stamina bar
                model.player.background.opacity -= Time.deltaTime * staminaBarFadeInDelay;
                model.player.fill.opacity = model.player.background.opacity;
            }
        }
    }
}