public enum PlayerState
{
    moving,
    ui,
    talking,
    assembling, // Player is assembling a product, can be used to disable movement and interaction while assembling
    placingPC
}
