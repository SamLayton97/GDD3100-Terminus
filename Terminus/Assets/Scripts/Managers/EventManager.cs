using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Facilitates adding invokers and listeners to various in-game events
/// </summary>
public static class EventManager
{
    #region Activate Player Fire Feedback

    // delcare lists to hold invokers and listeners to Activate Player Fire Feedback event
    static List<Weapon> fireFeedbackInvokers = new List<Weapon>();
    static List<UnityAction<float, WeaponType>> fireFeedbackListeners = new List<UnityAction<float, WeaponType>>();

    // Adds given weapons as invoker of fire feedback event
    public static void AddFireFeedbackInvoker(Weapon invoker)
    {
        // adds invoker to list and adds all listeners to this invoekr
        fireFeedbackInvokers.Add(invoker);
        foreach (UnityAction<float, WeaponType> listener in fireFeedbackListeners)
            invoker.AddFireFeedbackListener(listener);
    }

    // Adds given method as listener to fire feedback event
    public static void AddFireFeedbackListener(UnityAction<float, WeaponType> listener)
    {
        // adds listener to list and to all invokers of event
        fireFeedbackListeners.Add(listener);
        foreach (Weapon invoker in fireFeedbackInvokers)
            invoker.AddFireFeedbackListener(listener);
    }

    #endregion

    #region Pick Up Weapon

    // declare lists to hold invokers and listeners to Pick Up Weapon event
    static List<WeaponAdder> pickUpWeaponInvokers = new List<WeaponAdder>();
    static List<UnityAction<WeaponType>> pickUpWeaponListeners = new List<UnityAction<WeaponType>>();

    // Adds given weapon pickup as invoker of pick up weapon event
    public static void AddPickUpWeaponInvoker(WeaponAdder invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        pickUpWeaponInvokers.Add(invoker);
        foreach (UnityAction<WeaponType> listener in pickUpWeaponListeners)
            invoker.AddPickUpWeaponListener(listener);
    }

    // Adds given method as listener to Pick Up Weapon event
    public static void AddPickUpWeaponListener(UnityAction<WeaponType> listener)
    {
        // adds listener to list and to all invokers of event
        pickUpWeaponListeners.Add(listener);
        foreach (WeaponAdder invoker in pickUpWeaponInvokers)
            invoker.AddPickUpWeaponListener(listener);
    }

    #endregion

    #region Empty Weapon

    // declare lists to hold invokers and listeners to Empty Weapon event
    static List<Weapon> emptyWeaponInvokers = new List<Weapon>();
    static List<UnityAction> emptyWeaponListeners = new List<UnityAction>();

    // Adds given weapon as invoker of Empty Weapon event
    public static void AddEmptyWeaponInvoker(Weapon invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        emptyWeaponInvokers.Add(invoker);
        foreach (UnityAction listener in emptyWeaponListeners)
            invoker.AddEmptyWeaponListener(listener);
    }

    // Adds given method as listener to Empty Weapon Event
    public static void AddEmptyWeaponListener(UnityAction listener)
    {
        // adds listener to list and to all invokers of event
        emptyWeaponListeners.Add(listener);
        foreach (Weapon invoker in emptyWeaponInvokers)
            invoker.AddEmptyWeaponListener(listener);
    }

    #endregion

    #region Add Crafting Materials

    // declare lists to hold invokers and listeners to Pick Up Materials event
    static List<CraftingMaterialAdder> pickUpMaterialsInvokers = new List<CraftingMaterialAdder>();
    static List<UnityAction<CraftingMaterials, int>> pickupMaterialsListeners = 
        new List<UnityAction<CraftingMaterials, int>>();

    // Adds given crafting material adder as invoker of pickup materials event
    public static void AddPickUpMaterialsInvoker(CraftingMaterialAdder invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        pickUpMaterialsInvokers.Add(invoker);
        foreach (UnityAction<CraftingMaterials, int> listener in pickupMaterialsListeners)
            invoker.AddPickUpMaterialsListener(listener);
    }

    // Adds given method as listener to pick up materials event
    public static void AddPickUpMaterialsListener(UnityAction<CraftingMaterials, int> listener)
    {
        // adds listener to list and to all invokers of event
        pickupMaterialsListeners.Add(listener);
        foreach (CraftingMaterialAdder invoker in pickUpMaterialsInvokers)
            invoker.AddPickUpMaterialsListener(listener);
    }

    #endregion

    #region Remove Crafting Materials

    // declare lists to hold invokers and listeners to Remove Materials event
    static List<CraftingMaterialHolder> removeMaterialsInvokers = new List<CraftingMaterialHolder>();
    static List<UnityAction<CraftingMaterials, int>> removeMaterialsListeners = new List<UnityAction<CraftingMaterials, int>>();

    // Adds given remover as invoker of remove materials event
    public static void AddRemoveMaterialsInvoker(CraftingMaterialHolder invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        removeMaterialsInvokers.Add(invoker);
        foreach (UnityAction<CraftingMaterials, int> listener in removeMaterialsListeners)
            invoker.AddRemoveMaterialsListener(listener);
    }

    // Adds given method as listener to remove materials event
    public static void AddRemoveMaterialsListener(UnityAction<CraftingMaterials, int> listener)
    {
        // adds listener to list and to all invokers of event
        removeMaterialsListeners.Add(listener);
        foreach (CraftingMaterialHolder invoker in removeMaterialsInvokers)
            invoker.AddRemoveMaterialsListener(listener);
    }

    #endregion

    #region Update Crafting Materials UI

    // declare lists to hold invokers and listeners to Update Crafting Materials UI event
    static List<CraftingMaterialsInventory> updateMaterialsUIInvokers = new List<CraftingMaterialsInventory>();
    static List<UnityAction<CraftingMaterials, int>> updateMaterialsUIListeners = 
        new List<UnityAction<CraftingMaterials, int>>();

    // Adds given crafting materials inventory as invoker of update materials UI event
    public static void AddUpdateMaterialsUIInvoker(CraftingMaterialsInventory invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        updateMaterialsUIInvokers.Add(invoker);
        foreach (UnityAction<CraftingMaterials, int> listener in updateMaterialsUIListeners)
            invoker.AddUpdateMaterialsUIEvent(listener);
    }

    // Adds given method as listener to update crafting materials UI event
    public static void AddUpdateMaterialsUIListener(UnityAction<CraftingMaterials, int> listener)
    {
        // adds listener to list and to all invokers of event
        updateMaterialsUIListeners.Add(listener);
        foreach (CraftingMaterialsInventory invoker in updateMaterialsUIInvokers)
            invoker.AddUpdateMaterialsUIEvent(listener);
    }

    #endregion

    #region Pop Material From Crafting Menu

    // declare lists to hold invokers and listeners to Pop Material event
    static List<CraftingMaterialOnDeck> popMaterialInvokers = new List<CraftingMaterialOnDeck>();
    static List<UnityAction<CraftingMaterials>> popMaterialListeners = new List<UnityAction<CraftingMaterials>>();

    // Adds given material on deck as invoker of pop material event
    public static void AddPopMaterialInvoker(CraftingMaterialOnDeck invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        popMaterialInvokers.Add(invoker);
        foreach (UnityAction<CraftingMaterials> listener in popMaterialListeners)
            invoker.AddPopMaterialListener(listener);
    }

    // Adds given method as listener to pop materials event
    public static void AddPopMaterialsListener(UnityAction<CraftingMaterials> listener)
    {
        // adds listener to list and to all invokers of event
        popMaterialListeners.Add(listener);
        foreach (CraftingMaterialOnDeck invoker in popMaterialInvokers)
            invoker.AddPopMaterialListener(listener);
    }

    #endregion

    #region Swap Weapon UI

    // declare lists to hold invokers and listeners of Swap Weapon UI event
    static List<WeaponSelect> swapWeaponUIInvokers = new List<WeaponSelect>();
    static List<UnityAction<int>> swapWeaponUIListeners = new List<UnityAction<int>>();

    // Adds given weapon select as invoker of Swap Weapon UI event
    public static void AddSwapWeaponUIInvoker(WeaponSelect invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        swapWeaponUIInvokers.Add(invoker);
        foreach (UnityAction<int> listener in swapWeaponUIListeners)
            invoker.AddSwapWeaponUIListener(listener);
    }

    // Adds given method as listener to Swap Weapon UI event
    public static void AddSwapWeaponUIListener(UnityAction<int> listener)
    {
        // adds listener to list and to all invokers of event
        swapWeaponUIListeners.Add(listener);
        foreach (WeaponSelect invoker in swapWeaponUIInvokers)
            invoker.AddSwapWeaponUIListener(listener);
    }

    #endregion

    #region Update Ammo UI

    // declare lists to hold invokers and listeners of Update Ammo UI event
    static List<Weapon> updateAmmoUIInvokers = new List<Weapon>();
    static List<UnityAction<WeaponType, float>> updateAmmoUIListeners = new List<UnityAction<WeaponType, float>>();

    // Adds given weapon as invoker of update ammo ui event
    public static void AddUpdateAmmoUIInvoker(Weapon invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        updateAmmoUIInvokers.Add(invoker);
        foreach (UnityAction<WeaponType, float> listener in updateAmmoUIListeners)
            invoker.AddUpdateAmmoUIListener(listener);
    }

    // Adds given method as listener to update ammo ui event
    public static void AddUpdateAmmoUIListener(UnityAction<WeaponType, float> listener)
    {
        // adds listener to list and to all invokers of event
        updateAmmoUIListeners.Add(listener);
        foreach (Weapon invoker in updateAmmoUIInvokers)
            invoker.AddUpdateAmmoUIListener(listener);
    }

    #endregion

    #region Update O2 Gauge

    // declare lists to hold invokers and listeners to update O2 display event
    static List<OxygenControl> updateO2Invokers = new List<OxygenControl>();
    static List<UnityAction<float>> updateO2Listeners = new List<UnityAction<float>>();

    // Adds given oxygen controller as invoker of update O2 event
    public static void AddUpdateO2Invoker(OxygenControl invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        updateO2Invokers.Add(invoker);
        foreach (UnityAction<float> listener in updateO2Listeners)
            invoker.AddUpdateO2Listener(listener);
    }

    // Adds given method as listener to update O2 event
    public static void AddUpdateO2Listener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        updateO2Listeners.Add(listener);
        foreach (OxygenControl invoker in updateO2Invokers)
            invoker.AddUpdateO2Listener(listener);
    }

    #endregion

    #region Update Sanity Gauge

    // declare lists to hold invokers and listeners to update sanity display event
    static List<SanityControl> updateSanityInvokers = new List<SanityControl>();
    static List<UnityAction<float>> updateSanityListeners = new List<UnityAction<float>>();

    // Adds given sanity controller as invoker of update sanity event
    public static void AddUpdateSanityInvoker(SanityControl invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        updateSanityInvokers.Add(invoker);
        foreach (UnityAction<float> listener in updateSanityListeners)
            invoker.AddUpdateSanityListener(listener);
    }

    // Adds given method as listener to update sanity event
    public static void AddUpdateSanityListener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        updateSanityListeners.Add(listener);
        foreach (SanityControl invoker in updateSanityInvokers)
            invoker.AddUpdateSanityListener(listener);
    }

    #endregion

    #region Refill Player O2

    // declare lists to hold invokers and listeners to refill player O2 event
    static List<RefillO2OnCollision> refillO2Invokers = new List<RefillO2OnCollision>();
    static List<UnityAction<float>> refillO2Listeners = new List<UnityAction<float>>();

    // Adds given O2 refiller as invoker of Refill Player O2 event
    public static void AddRefillO2Invoker(RefillO2OnCollision invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        refillO2Invokers.Add(invoker);
        foreach (UnityAction<float> listener in refillO2Listeners)
            invoker.AddRefillO2Listener(listener);
    }

    // Adds given method as listener to refill player O2 event
    public static void AddRefillO2Listener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        refillO2Listeners.Add(listener);
        foreach (RefillO2OnCollision invoker in refillO2Invokers)
            invoker.AddRefillO2Listener(listener);
    }

    #endregion

    #region Deduct Player O2

    // declare lists to hold invokers and listeners to deduct player O2 event
    static List<O2Remover> deductO2Invokers = new List<O2Remover>();
    static List<UnityAction<float, bool>> deductO2Listeners = new List<UnityAction<float, bool>>();

    // Adds given O2 remover as invoker of Deduct Player O2 event
    public static void AddDeductO2Invoker(O2Remover invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        deductO2Invokers.Add(invoker);
        foreach (UnityAction<float, bool> listener in deductO2Listeners)
            invoker.AddDeductO2Listener(listener);
    }

    // Adds given method as listener to Deduct Player O2 event
    public static void AddDeductO2Listener(UnityAction<float, bool> listener)
    {
        // adds listener to list and to all invokers of event
        deductO2Listeners.Add(listener);
        foreach (O2Remover invoker in deductO2Invokers)
            invoker.AddDeductO2Listener(listener);
    }

    #endregion

    #region Deduct Player Sanity

    // declare lists to hold invokers and listener to deduct sanity on fire event
    static List<SanityDeductor> deductSanityOnFireInvokers = new List<SanityDeductor>();
    static List<UnityAction<float>> deductSanityOnFireListeners = new List<UnityAction<float>>();

    // Adds given deductor as invoker of Deduct Sanity Event
    public static void AddDeductSanityInvoker(SanityDeductor invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        deductSanityOnFireInvokers.Add(invoker);
        foreach (UnityAction<float> listener in deductSanityOnFireListeners)
            invoker.AddDeductListener(listener);
    }

    // Adds given method as listener to Deduct Sanity event
    public static void AddDeductSanityListener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        deductSanityOnFireListeners.Add(listener);
        foreach (SanityDeductor invoker in deductSanityOnFireInvokers)
            invoker.AddDeductListener(listener);
    }

    #endregion

    #region Transition Scene

    // declare lists to hold invokers and listeners to transition scene event
    static List<SceneTransitioner> transitionSceneInvokers = new List<SceneTransitioner>();
    static List<UnityAction<string>> transitionSceneListeners = new List<UnityAction<string>>();

    // Adds given Scene Transitioner as invoker of Transition Scene Event
    public static void AddTransitionSceneInvoker(SceneTransitioner invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        transitionSceneInvokers.Add(invoker);
        foreach (UnityAction<string> listener in transitionSceneListeners)
            invoker.AddTransitionSceneListener(listener);
    }

    // Adds given method as listener to Transition Scene event
    public static void AddTransitionSceneListener(UnityAction<string> listener)
    {
        // adds listener to list and to all invokers of event
        transitionSceneListeners.Add(listener);
        foreach (SceneTransitioner invoker in transitionSceneInvokers)
            invoker.AddTransitionSceneListener(listener);
    }

    #endregion

    #region Toggle Pause

    // declare lists to hold invokers and listeners to toggle pause event
    static List<PopupControl> togglePauseInvokers = new List<PopupControl>();
    static List<UnityAction<bool>> togglePauseListeners = new List<UnityAction<bool>>();

    // Adds given pause controller as invoker of toggle pause event
    public static void AddTogglePauseInvoker(PopupControl invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        togglePauseInvokers.Add(invoker);
        foreach (UnityAction<bool> listener in togglePauseListeners)
            invoker.AddTogglePauseListener(listener);
    }

    // Adds given method as listener to toggle pause event
    public static void AddTogglePauseListener(UnityAction<bool> listener)
    {
        // adds listener to list and to all invokers of event
        togglePauseListeners.Add(listener);
        foreach (PopupControl invoker in togglePauseInvokers)
            invoker.AddTogglePauseListener(listener);
    }

    #endregion

    #region End Level

    // declare lists to hold invokers and listeners to end level event
    static List<LevelEnder> endLevelInvokers = new List<LevelEnder>();
    static List<UnityAction<bool, float>> endLevelListeners = new List<UnityAction<bool, float>>();

    // Adds given level ender as invoker of end level event
    public static void AddEndLevelInvoker(LevelEnder invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        endLevelInvokers.Add(invoker);
        foreach (UnityAction<bool, float> listener in endLevelListeners)
            invoker.AddEndLevelListener(listener);
    }

    // Adds given method as listener to end level event
    public static void AddEndLevelListener(UnityAction<bool, float> listener)
    {
        // adds listener to list and to all invokers of event
        endLevelListeners.Add(listener);
        foreach (LevelEnder invoker in endLevelInvokers)
            invoker.AddEndLevelListener(listener);
    }

    #endregion

}
