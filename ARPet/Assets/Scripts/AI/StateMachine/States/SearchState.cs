using System;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : IState
{
    private Vector3 searchPosition;
    private LayerMask searchLayer;
    private readonly float searchRadius;
    private readonly string searchTag;

    private bool isSearchComleted;
    private readonly Action<SearchResult> searchResultCallback;

    public SearchState
        (Vector3 searchPosition, 
        LayerMask searchLayer, 
        float searchRadius, 
        string searchTag, 
        Action<SearchResult> searchResultCallback)
    {
        this.searchPosition = searchPosition;
        this.searchLayer = searchLayer;
        this.searchRadius = searchRadius;
        this.searchTag = searchTag;

        this.searchResultCallback = searchResultCallback;
    }

    public void Enter()
    {
        isSearchComleted = false;
    }

    public void Execute()
    {
        if (!isSearchComleted)
        {
            var hitObjects = Physics.OverlapSphere(searchPosition, searchRadius, searchLayer);
            var allObjectsWithSearchTag = new Stack<Collider>();

            SearchAndStoreObjectsWithTag(hitObjects, allObjectsWithSearchTag);

            var searchResult = new SearchResult(hitObjects, allObjectsWithSearchTag);

            searchResultCallback(searchResult);

            isSearchComleted = true;
        } 
    }

    public void Exit()
    {

    }

    private void SearchAndStoreObjectsWithTag(Collider[] hitObjects, Stack<Collider> allObjectsWithSearchTag)
    {
        foreach (var hitObject in hitObjects)
        {
            if (hitObject.CompareTag(searchTag))
            {
                allObjectsWithSearchTag.Push(hitObject);
            }
        }
    }
}

public class SearchResult
{
    public Collider[] AllHitCollidersInRadius;
    public Stack<Collider> AllObjectsWithSearchTag;

    public SearchResult(Collider[] allHitCollidersInRadius, Stack<Collider> allObjectsWithSearchTag)
    {
        this.AllHitCollidersInRadius = allHitCollidersInRadius;
        this.AllObjectsWithSearchTag = allObjectsWithSearchTag;
    }
}
