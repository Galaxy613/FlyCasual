using Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrade;

namespace UpgradesList
{

    public class SalvagedAstromech : GenericUpgrade
    {

        public SalvagedAstromech() : base()
        {
            Type = UpgradeType.SalvagedAstromech;
            Name = "Salvaged Astromech";
            Cost = 2;

            UpgradeAbilities.Add(new Abilities.SalvagedAstromechAbility());
        }
        
    }

}


namespace Abilities
{
    public class SalvagedAstromechAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            HostShip.OnDamageCardIsDealt += RegisterSalvagedAstromechTrigger;
        }

        public override void DeactivateAbility()
        {
            HostShip.OnDamageCardIsDealt -= RegisterSalvagedAstromechTrigger;
        }

        private void RegisterSalvagedAstromechTrigger(GenericShip ship)
        {
            RegisterAbilityTrigger(TriggerTypes.OnDamageCardIsDealt, AskUseSalvagedAstromechAbility);
        }

        private void AskUseSalvagedAstromechAbility(object sender, System.EventArgs e)
        {
            GenericShip previousShip = Selection.ActiveShip;
            Selection.ActiveShip = sender as GenericShip;

            AskToUseAbility(
                IsShouldUseAbility,
                UseAbility,
                delegate
                {
                    Selection.ActiveShip = previousShip;
                    Triggers.FinishTrigger();
                });
        }

        private bool IsShouldUseAbility()
        {
            bool result = false;
            
            if (Combat.CurrentCriticalHitCard.IsFaceUp &&
                Combat.CurrentCriticalHitCard.Type == CriticalCardType.Ship) result = true;

            return result;
        }

        private void UseAbility(object sender, System.EventArgs e)
        {
            GenericUpgrade astromech = HostShip.UpgradeBar.GetInstalledUpgrades().Find(n => n.Type == UpgradeType.SalvagedAstromech);
            Sounds.PlayShipSound("R2D2-Killed");
            Messages.ShowInfo("Salvaged Astromech is used & discarded");
            Combat.CurrentCriticalHitCard = null;
            astromech.Discard(DiscardModification);
        }

        private void DiscardModification()
        {
            HostUpgrade.Discard(SubPhases.DecisionSubPhase.ConfirmDecision);
        }

    }
}