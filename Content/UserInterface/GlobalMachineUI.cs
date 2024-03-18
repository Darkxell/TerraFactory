using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using TerraFactory.Content.Tiles.Machines;
using TerraFactory.Content.Utils;
using Terraria.DataStructures;

namespace TerraFactory.Content.UserInterface
{
    /// <summary>
    /// Manages the global machine UI
    /// </summary>
    [Autoload(Side = ModSide.Client)]
    public class GlobalMachineUI : ModSystem
    {

        internal GlobalMachineUIState state;
        private Terraria.UI.UserInterface _state;

        /// <summary>
        /// The current display target of this interface. 
        /// May be null, in which case the UI should close.
        /// </summary>
        public AbstractMachineTE currentDisplay = null;

        public override void Load()
        {
            state = new GlobalMachineUIState();
            state.Activate();

            _state = new Terraria.UI.UserInterface();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (currentDisplay == null)
            {
                HideMachineUI();
            }
            else
            {
                // If player is far from the machine he opened, close the interface
                if (Main.player[Main.myPlayer].position.DistanceSQ(currentDisplay.Position.ToVector2() * 16) > 65_536 /* 16 blocks : (16*16)^2=65_536 */)
                {
                    HideMachineUI();
                }
                else
                {
                    // If all checks pass, update the current UI with the potentially new TE state
                    state.setInfoFrom(currentDisplay);
                }
            }
            // Update all childs
            _state?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "TerraFactory: Global Machine UI",
                    delegate
                    {
                        _state.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public void ShowMachineUI()
        {
            _state?.SetState(state);
            isShowing = true;
        }

        public void ShowMachineUI(AbstractMachineTE target)
        {
            currentDisplay = target;
            state.setInfoFrom(target);
            ShowMachineUI();
        }

        public void HideMachineUI()
        {
            _state?.SetState(null);
            isShowing = false;
            currentDisplay = null;
        }

        private bool isShowing = false;
        public bool IsShowing()
        {
            return isShowing;
        }

        public bool IsShowing(AbstractMachineTE compare)
        {
            return isShowing && currentDisplay == compare;
        }
    }

    /// <summary>
    /// The UI State of the whole Machine UI
    /// </summary>
    class GlobalMachineUIState : UIState
    {
        UIPanel background;
        UIText header1;
        UIText recepietext;
        UIText inventoryList;

        public override void OnInitialize()
        {
            background = new UIPanel();
            background.Width.Set(487, 0);
            background.Height.Set(276, 0);
            background.Top.Set(320f, 0);
            background.Left.Set(80f, 0);
            Append(background);

            header1 = new UIText("Machine name", textScale: 1.5f);
            header1.HAlign = 0.5f;
            header1.Top.Set(10, 0);
            background.Append(header1);

            UIText header2 = new UIText("Inventory", textScale: 1.5f);
            header2.HAlign = 0.5f;
            header2.Top.Set(100, 0);
            background.Append(header2);

            inventoryList = new UIText("");
            inventoryList.HAlign = 0.5f;
            inventoryList.Top.Set(135, 0);
            background.Append(inventoryList);

            recepietext = new UIText("Possible item inputs : ");
            recepietext.Left.Set(30, 0);
            recepietext.Top.Set(50, 0);
            background.Append(recepietext);

            // Deposit items button
            UIPanel depositbutton = new DepositButton();
            depositbutton.Width.Set(50, 0);
            depositbutton.Height.Set(50, 0);
            depositbutton.Left.Set(30, 0);
            depositbutton.Top.Set(185, 0);
            depositbutton.OnLeftClick += deposit;
            background.Append(depositbutton);

            UIText depositText = new UIText("Deposit");
            depositText.Left.Set(30, 0);
            depositText.Top.Set(170, 0);
            background.Append(depositText);

            // Take all items button
            UIPanel takeallbutton = new UIPanel();
            takeallbutton.Width.Set(100, 0);
            takeallbutton.Height.Set(50, 0);
            takeallbutton.Left.Set(100, 0);
            takeallbutton.Top.Set(185, 0);
            takeallbutton.OnLeftClick += empty;
            background.Append(takeallbutton);

            UIText takeAllText = new UIText("Take all");
            takeAllText.HAlign = takeAllText.VAlign = 0.5f;
            takeallbutton.Append(takeAllText);

            // Close pannel button
            UIPanel closebutton = new UIPanel();
            closebutton.Width.Set(100, 0);
            closebutton.Height.Set(50, 0);
            closebutton.Left.Set(340, 0);
            closebutton.Top.Set(185, 0);
            closebutton.OnLeftClick += close;
            background.Append(closebutton);

            UIText closetext = new UIText("Close");
            closetext.HAlign = closetext.VAlign = 0.5f;
            closebutton.Append(closetext);
        }

        public void setInfoFrom(AbstractMachineTE target)
        {
            header1.SetText(target.getDisplayName());

            // Displays Item input list
            StringBuilder inputs = new StringBuilder("Possible item inputs : ");
            int[] mIn = target.getItemInputsList();
            foreach (int i in mIn) inputs.Append("[i: " + i + "]");
            recepietext.SetText(inputs.ToString());

            // Displays machine content
            StringBuilder inventory = new StringBuilder();
            if (target.content.Count <= 0)
                inventory.Append("Machine is empty");
            else
                foreach (FastItemStack itm in target.content)
                    inventory.Append(itm.toString() + " ");
            inventoryList.SetText(inventory.ToString());
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            // Block mouse item usage if the cursor is contained by the background panel
            if (background.ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            // TODO : add a machine current craft progress bar here





        }

        public void close(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            ModContent.GetInstance<GlobalMachineUI>().HideMachineUI();
        }

        /// <summary>
        /// Deposits the cursor item in the currently opened machine
        /// </summary>
        public void deposit(UIMouseEvent evt, UIElement listeningElement)
        {
            Item cursoritem = Main.player[Main.myPlayer].inventory[58];
            if (cursoritem == null || cursoritem.stack <= 0) return;
            AbstractMachineTE destination = ModContent.GetInstance<GlobalMachineUI>().currentDisplay;
            if (destination == null) return;

            destination.addFastItem(new FastItemStack(cursoritem.type, cursoritem.stack));
            cursoritem.stack = 0;
            Main.mouseItem.stack = 0;
        }

        /// <summary>
        /// Empties this machine and dumps its content on the floor in item form
        /// </summary>
        public void empty(UIMouseEvent evt, UIElement listeningElement)
        {
            AbstractMachineTE source = ModContent.GetInstance<GlobalMachineUI>().currentDisplay;
            if (source == null || source.content == null || source.content.Count() <= 0) return;

            Tile tile = Main.tile[source.Position.X, source.Position.Y];
            if (tile == null || !tile.HasTile) return;
            ModTile mtile = TileLoader.GetTile(tile.TileType);

            if (mtile is not AbstractMachine) return;
            AbstractMachine machine = (AbstractMachine)mtile;

            foreach (FastItemStack item in source.content)
            {
                Item.NewItem(null, new Vector2((source.Position.X + machine.machine_Width / 2) * 16, (source.Position.Y + machine.machine_Height / 2) * 16),
                    item.itemID, Stack: item.quantity, noGrabDelay: true);
            }
            source.content.Clear();
        }
    }

    class DepositButton : UIPanel
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsMouseHovering)
            {
                Main.hoverItemName = "LPlace an item in this slot to deposit into the machine";
            }
        }
    }
}
