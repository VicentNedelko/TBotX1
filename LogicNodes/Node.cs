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

        [Output(DisplayOrder = 1)]
        public BoolValueObject BotTestOutput { get; set; }

        [Output(DisplayOrder = 2)]
        public StringValueObject ChatIdOutput { get; set; }

        [Output(DisplayOrder = 3)]
        public BoolValueObject MessageTest { get; set; }


        public Node(INodeContext context)
          : base(context)
        {
          context.ThrowIfNull("context");

            this.typeService = context.GetService<ITypeService>();
            this.Message = typeService.CreateString(PortTypes.String, "Message", "Empty");
            this.Send = typeService.CreateBool(PortTypes.Binary, "Send", false);
            this.BotTestOutput = typeService.CreateBool(PortTypes.Binary, "BotTest", false);
            this.ChatIdOutput = typeService.CreateString(PortTypes.String, "ChatId", "0");
            this.MessageTest = typeService.CreateBool(PortTypes.Binary, "MessageTest", false);
        }
    
        public override void Startup()
        {
            bot = new TBot();
            if (bot != null)
            {
                BotTestOutput.Value = true;   // check bot creation
            }
        }

        public override async void Execute()
        {
            
            if (this.Send.WasSet && this.Send.Value)
            {
                MessageTest.Value = true;
                ChatIdOutput.Value = (await bot.SendMessageAsync(Message)).ToString(); // message FB state OnSuccess
                ChatIdOutput.BlockGraph();
            }
        }
      }
}
