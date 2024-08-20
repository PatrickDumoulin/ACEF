$(document).ready(function () {
    // Fonction pour afficher/cacher les champs liés au "Petit Prêt"
    function toggleLoanFields() {
        var containsPetitPret = false;

        // Retirer les solutions déjà sélectionnées de la liste déroulante lors du chargement de la page
        $('#selectedSolutionsList li').each(function () {
            var solutionValue = $(this).data('value');
            console.log("ALLO" + solutionValue);

            // Supprimer l'option correspondante de la liste déroulante
            var optionToRemove = $('#AvailableSolutions option').filter(function () {
                return $(this).val() == solutionValue;
            });

            optionToRemove.remove();
        });

        $('#selectedSolutionsList li').each(function () {
            var solutionText = $(this).data('text');
            

            if (solutionText && solutionText.trim() === 'Petit Prêt') {
                containsPetitPret = true;
            }
        });

        if (containsPetitPret) {
            $('#loanFields').removeClass('hidden');
        } else {
            $('#loanFields').addClass('hidden');
            // Réinitialiser les champs si "petit prêt" est retiré
            $('#loanFields').find('select, input[type=radio]').val(null).prop('checked', false);
        }
    }

    // Appeler la fonction une fois au chargement
    toggleLoanFields();

    // Ajouter une solution à la liste des solutions proposées
    $('#addSolutionButton').on('click', function () {
        var selectedOption = $('#AvailableSolutions option:selected');
        var solutionText = selectedOption.text();
        var solutionValue = selectedOption.val();

        console.log("Adding solution:", solutionValue, solutionText);

        if (solutionValue && solutionText) {
            // Ajouter la solution à la liste des solutions proposées
            $('#selectedSolutionsList').append('<li class="list-group-item" data-value="' + solutionValue + '" data-text="' + solutionText + '">' +
                solutionText + ' <button type="button" class="btn btn-sm btn-danger float-right remove-solution"><i class="bi bi-trash"></i></button>' +
                '<input type="hidden" name="SelectedSolutions" value="' + solutionValue + '" />' +
                '</li>');

            // Retirer l'option sélectionnée de la liste déroulante
            selectedOption.remove();
            console.log(selectedOption)

            // Réinitialiser la sélection dans la liste déroulante
            $('#AvailableSolutions').val('');

            toggleLoanFields(); // Vérifier les champs liés au "Petit Prêt"

            
        }
    });

    // Supprimer une solution de la liste sélectionnée et la réajouter à la liste déroulante
    $('#selectedSolutionsList').on('click', '.remove-solution', function () {
        console.log('Suppression de la solution cliquée');
        var listItem = $(this).closest('li');
        var solutionValue = listItem.data('value');
        var solutionText = listItem.data('text'); // Utilisation de data-text pour récupérer le texte

        if (solutionValue && solutionText) {
            // Réajouter l'option supprimée dans la liste déroulante
            $('#AvailableSolutions').append('<option value="' + solutionValue + '">' + solutionText + '</option>');

            // Supprimer l'élément de la liste des solutions proposées
            listItem.remove();

            toggleLoanFields(); // Vérifier les champs liés au "Petit Prêt"
        }
    });

    // Validation côté client pour vérifier que IdClient est défini et qu'au moins une solution est sélectionnée
    $('form').on('submit', function (event) {
        var clientId = $('#IdClient').val();
        var selectedSolutions = $('#selectedSolutionsList li').length;

        if (!clientId) {
            event.preventDefault();
            alert("Veuillez sélectionner un dossier client.");
            return false;
        }

        if (selectedSolutions === 0) {
            event.preventDefault();
            alert("Veuillez sélectionner au moins une solution.");
            return false;
        }

        return true;
    });

    // Recherche et sélection d'un client
    $('#searchClientInput').on('input', function () {
        var searchTerm = $(this).val().toLowerCase();
        if (searchTerm.length > 2) {
            $.ajax({
                url: '/Intervention/SearchClients',
                data: { searchTerm: searchTerm },
                success: function (data) {
                    $('#clientSearchResults').empty();
                    data.forEach(function (client) {
                        $('#clientSearchResults').append('<li class="list-group-item" data-id="' + client.id + '">' + client.fullName + ' (#' + client.id + ')</li>');
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Erreur lors de la recherche des clients:", status, error);
                }
            });
        }
    });

    $('#clientSearchResults').on('click', 'li', function () {
        var clientId = $(this).data('id');
        var clientName = $(this).text();
        $('#IdClient').val(clientId);
        $('#clientNameDisplay').val(clientName);
        $('#clientModal').modal('hide');
    });

    // Nettoyage du modal de recherche de client
    $('#clientModal').on('hidden.bs.modal', function () {
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        $('#clientSearchResults').empty();
        $('#clientSearchResults').empty();
        $('body').css('overflow', 'auto');
    });


});
