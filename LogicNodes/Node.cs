using gira_com_by.Logic.Nodes.Models;
using LogicModule.Nodes.Helpers;
using LogicModule.ObjectModel;
using LogicModule.ObjectModel.TypeSystem;
using System.Threading.Tasks;

namespace gira_com_by.Logic.Nodes
{
  public class Node : LogicNodeBase
  {
        private readonly ITypeService typeService;
        private IBot bot;

        [Parameter(DisplayOrder = 1, IsRequired = true)]
        public StringValueObject Message { get; private set; }

        [Input(DisplayOrder = 2, IsDefaultShown = false, IsInput = true)]
        public BoolValueObject Send { get; private set; }





        public Node(INodeContext context)
          : base(context)
        {
            context.ThrowIfNull("context");

            this.typeService = context.GetService<ITypeService>();
            this.Message = typeService.CreateString(PortTypes.String, "Message", "Empty");
            this.Send = typeService.CreateBool(PortTypes.Binary, "Send", false);
        }
    
        public override void Startup()
        {
            //bot = new TBot();
        }

        public override async void Execute()
        {
            
            if (this.Send.WasSet && this.Send.Value)
            {
                bot = new TBot();
                _ = await bot.SendMessageAsync(Message); // message FB state OnSuccess
            }
        }
  }

}
