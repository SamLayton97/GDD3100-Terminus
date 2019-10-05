using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Facilitates adding invokers and listeners to various in-game events
/// </summary>
public static class EventManager
{
    #region Pick Up Weapon

    // declare lists to hold invokers and listeners to Pick Up Weapon event
    static List<AddWeaponOnCollision> pickUpWeaponInvokers = new List<AddWeaponOnCollision>();
    static List<UnityAction<WeaponType>> pickUpWeaponListeners = new List<UnityAction<WeaponType>>();

    // Adds given weapon pickup as invoker of pick up weapon event
    public static void AddPickUpWeaponInvoker(AddWeaponOnCollision invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        pickUpWeaponInvokers.Add(invoker);
        foreach (UnityAction<WeaponType> listener in pickUpWeaponListeners)
            invoker.AddPickupWeaponInvoker(listener);
    }

    // Adds given method as listener to Pick Up Weapon event
    public static void AddPickUpWeaponListener(UnityAction<WeaponType> listener)
    {
        // adds listener to list and to all invokers of event
        pickUpWeaponListeners.Add(listener);
        foreach (AddWeaponOnCollision invoker in pickUpWeaponInvokers)
            invoker.AddPickupWeaponInvoker(listener);
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

    #region Pick Up Crafting Materials

    // declare lists to hold invokers and listeners to Pick Up Materials event
    static List<AddCraftingMaterialOnCollision> pickUpMaterialsInvokers = new List<AddCraftingMaterialOnCollision>();
    static List<UnityAction<CraftingMaterials, int>> pickupMaterialsListeners = 
        new List<UnityAction<CraftingMaterials, int>>();

    // Adds given crafting material adder as invoker of pickup materials event
    public static void AddPickUpMaterialsInvoker(AddCraftingMaterialOnCollision invoker)
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
        foreach (AddCraftingMaterialOnCollision invoker in pickUpMaterialsInvokers)
            invoker.AddPickUpMaterialsListener(listener);
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

    #region Deduct Player Sanity On Fire

    // declare lists to hold invokers and listener to deduct sanity on fire event
    static List<BioRifle> deductSanityOnFireInvokers = new List<BioRifle>();
    static List<UnityAction<float>> deductSanityOnFireListeners = new List<UnityAction<float>>();

    // Adds given biorifle as invoker of Deduct Sanity On Fire Event
    public static void AddDeductSanityOnFireInvoker(BioRifle invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        deductSanityOnFireInvokers.Add(invoker);
        foreach (UnityAction<float> listener in deductSanityOnFireListeners)
            invoker.AddDeductSanityInvoker(listener);
    }

    // Adds given method as listener to Deduct Sanity On Fire event
    public static void AddDeductSanityOnFireListener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        deductSanityOnFireListeners.Add(listener);
        foreach (BioRifle invoker in deductSanityOnFireInvokers)
            invoker.AddDeductSanityInvoker(listener);
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
