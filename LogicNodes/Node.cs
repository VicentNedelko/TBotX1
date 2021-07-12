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

        [Input(DisplayOrder = 2, IsInput = true, IsRequired = true)]
        public BoolValueObject Send { get; set; }

        [Output(DisplayOrder = 1)]
        public StringValueObject BotCreatedOutput { get; set; }

        [Output(DisplayOrder = 2)]
        public StringValueObject ChatIdOutput { get; set; }


        public Node(INodeContext context)
          : base(context)
        {
          context.ThrowIfNull("context");

            this.typeService = context.GetService<ITypeService>();
            this.Message = typeService.CreateString(PortTypes.String, "Message", "Empty");
            this.Send = typeService.CreateBool(PortTypes.Bool, "Send", false);
            this.BotCreatedOutput = typeService.CreateString(PortTypes.String, "BotCreated", "NULL");
            this.ChatIdOutput = typeService.CreateString(PortTypes.String, "ChatId", "0");
        }
    
        public override void Startup()
        {
            bot = new TBot();
            if(bot != null)
            {
                BotCreatedOutput.Value = "Created";   // check bot creation
            }
            else
            {
                BotCreatedOutput.Value = "Error! Bot is NOT created.";
            }
        }

        public override async void Execute()
        {
            if (Send.WasSet && Send.Value)
            {
                ChatIdOutput.Value = (await bot.SendMessageAsync(Message)).ToString(); // message FB state OnSuccess
                ChatIdOutput.BlockGraph();
            }
        }
      }
}
