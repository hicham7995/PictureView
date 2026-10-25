namespace PictureViewerTests
{
    using System.Windows.Input;
    using System.Windows.Threading;

    using NUnit.Framework;

    using PictureView;

    public class RelayCommandTests
    {
        [Test]
        [STAThread]
        public void CanExecute_ReturnsPredicateResult()
        {
            var command = new RelayCommand(
                _ => { },
                parameter => parameter is string value && value == "allowed");

            Assert.That(command.CanExecute("allowed"), Is.True);
            Assert.That(command.CanExecute("denied"), Is.False);
            Assert.That(command.CanExecute(null), Is.False);
        }

        [Test]
        [STAThread]
        public void CanExecuteChanged_IsRaisedWhenCommandManagerRequestsRequery()
        {
            var command = new RelayCommand(_ => { }, _ => true);
            var raised = false;
            //// permet de créer un boucle de message dans une sorte de fenêtre invisible pour que le test puisse attendre l'événement CanExecuteChanged
            var frame = new DispatcherFrame();

            EventHandler handler = (_, _) =>
            {
                raised = true;
                //// la boucle de message est terminée, on peut continuer le test
                frame.Continue = false;
            };

            command.CanExecuteChanged += handler;
            //// demande à CommandManager de re-évaluer CanExecute, ce qui devrait déclencher l'événement CanExecuteChanged
            CommandManager.InvalidateRequerySuggested();
            //// on doit exécuter la boucle de message pour que l'événement soit traité
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => { }));
            //// fait démarrer la boucle de message, qui sera arrêtée par le gestionnaire d'événements lorsque CanExecuteChanged sera déclenché
            Dispatcher.PushFrame(frame);

            Assert.That(raised, Is.True);

            command.CanExecuteChanged -= handler;
        }
    }
}
