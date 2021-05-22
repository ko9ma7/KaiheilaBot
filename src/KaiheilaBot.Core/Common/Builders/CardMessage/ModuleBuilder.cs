using System.Collections.Generic;
using KaiheilaBot.Core.Models.Objects.CardMessages;
using KaiheilaBot.Core.Models.Objects.CardMessages.Elements;
using KaiheilaBot.Core.Models.Objects.CardMessages.Enums;
using KaiheilaBot.Core.Models.Objects.CardMessages.Modules;

namespace KaiheilaBot.Core.Common.Builders.CardMessage
{
    public class ModuleBuilder
    {
        private readonly List<object> _modules = new();

        public ModuleBuilder AddModule(IModuleBase module)
        {
            _modules.Add(module);
            return this;
        }

        public ModuleBuilder AddActionGroup(IEnumerable<Button> buttons)
        {
            _modules.Add(new ActionGroup()
            {
                Elements = buttons
            });
            return this;
        }

        public ModuleBuilder AddAudio(string source, string title, string cover)
        {
            _modules.Add(new Audio()
            {
                Source = source,
                Title = title,
                Cover = cover
            });
            return this;
        }

        public ModuleBuilder AddContext(IEnumerable<IContextElement> contextElements)
        {
            _modules.Add(new Context()
            {
                Elements = contextElements
            });
            return this;
        }

        public ModuleBuilder AddCountdown(long startTime, long endTime, CountdownModes mode)
        {
            _modules.Add(new Countdown()
            {
                StartTime = startTime,
                EndTime = endTime,
                Mode = mode
            });
            return this;
        }

        public ModuleBuilder AddDivider()
        {
            _modules.Add(new Divider());
            return this;
        }

        public ModuleBuilder AddFile(string source, string title)
        {
            _modules.Add(new File()
            {
                Source = source,
                Title = title
            });
            return this;
        }

        public ModuleBuilder AddHeader(PlainText text)
        {
            _modules.Add(new Header()
            {
                Text = text
            });
            return this;
        }

        public ModuleBuilder AddImageGroup(IEnumerable<Image> images)
        {
            _modules.Add(new ImageGroup()
            {
                Elements = images
            });
            return this;
        }

        public ModuleBuilder AddSection(
            SectionModes mode, 
            ISectionText sectionTexts,
            object sectionAccessories)
        {
            _modules.Add(new Section()
            {
                Mode = mode,
                Text = sectionTexts,
                Accessory = sectionAccessories
            });
            return this;
        }

        public ModuleBuilder AddVideo(string source, string title)
        {
            _modules.Add(new Video()
            {
                Source = source,
                Title = title
            });
            return this;
        }

        public List<object> Build()
        {
            return _modules;
        }
    }
}
