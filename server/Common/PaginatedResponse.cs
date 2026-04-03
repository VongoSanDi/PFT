namespace server.Common;

/// <summary>
/// Représente une réponse paginée contenant les données demandées
/// ainsi que les métadonnées de pagination.
/// </summary>
/// <typeparam name="T">Type des éléments renvoyés dans la collection paginée.</typeparam>
public class PaginatedResponse<T>
{
    /// <summary>
    /// Contient les éléments de la page courante.
    /// </summary>
    public IReadOnlyList<T> Data { get; set; } = [];

    public required PaginationMetadata Metadata { get; set; }
}

public class PaginationMetadata
{
    public PaginationMetadata(int pageNumber, int pageSize, int totalRecords)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        HasNextPage = pageNumber < TotalPages;
        HasPreviousPage = pageNumber > 1;
    }

    /// <summary>
    /// Indique le numéro de la page courante.
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// Indique le nombre d’éléments demandés par page.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Indique le nombre total d’enregistrements disponibles.
    /// </summary>
    public int TotalRecords { get; init; }

    /// <summary>
    /// Indique le nombre total de pages disponibles.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Indique s’il existe une page suivante.
    /// </summary>
    public bool HasNextPage { get; init; }

    /// <summary>
    /// Indique s’il existe une page précédente.
    /// </summary>
    public bool HasPreviousPage { get; init; }
}
