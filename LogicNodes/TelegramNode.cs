using gira_com_by.Logic.Nodes.Models;
using LogicModule.Nodes.Helpers;
using LogicModule.ObjectModel;
using LogicModule.ObjectModel.TypeSystem;
using System.Threading.Tasks;

namespace gira_com_by.Logic.Nodes
{
  public class TelegramNode : LogicNodeBase
  {
        private readonly ITypeService typeService;
        private IBot bot;

        [Parameter(DisplayOrder = 1, IsRequired = true, InitOrder = 2)]
        public StringValueObject Message { get; private set; }

        [Input(DisplayOrder = 2, IsDefaultShown = false, IsInput = true, InitOrder = 1)]
        public BoolValueObject Send { get; private set; }

        [Output(DisplayOrder = 1)]
        public BoolValueObject Result { get; private set; }


        public TelegramNode(INodeContext context)
          : base(context)
        {
            context.ThrowIfNull("context");

            this.typeService = context.GetService<ITypeService>();
            this.Message = typeService.CreateString(PortTypes.String, "Message", "Empty");
            this.Send = typeService.CreateBool(PortTypes.Binary, "Send", false);
            this.Result = typeService.CreateBool(PortTypes.Switch, "Result");
        }
    
        public override void Startup()
        {
            bot = new TBot();
        }

        public override async void Execute()
        {
            if (this.Send.WasSet && this.Send.Value)
            {
                this.Result.Value = await bot.SendMessageAsync(Message);
            }
        }
  }

}
