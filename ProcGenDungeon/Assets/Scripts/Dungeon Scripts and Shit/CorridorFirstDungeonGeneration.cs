using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorridorFirstDungeonGeneration : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private GameObject skullSpawner;
    [SerializeField]
    private GameObject zombieSpawner;
    [SerializeField]
    private GameObject skeletonSpawner;
    [SerializeField]
    private GameObject bossPortal;
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

     protected override void RunProceduralGeneration() {
        CorridorFirstGeneration();
     }

     protected void CorridorFirstGeneration() {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        //Debug.Log("Number of potential room positions: " + potentialRoomPositions.Count);

        foreach(var rooms in potentialRoomPositions) {
            Instantiate(skullSpawner, new Vector3(rooms.x, rooms.y, 0), Quaternion.identity);
            Instantiate(zombieSpawner, new Vector3(rooms.x + 1, rooms.y - 1, 0), Quaternion.identity);
            Instantiate(skeletonSpawner, new Vector3(rooms.x + 2, rooms.y - 2, 0), Quaternion.identity);
        }

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        foreach (var deadEnd in deadEnds) {
            Instantiate(bossPortal, new Vector3(deadEnd.x + 15, deadEnd.y + 15, 0), Quaternion.identity);
        }

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for(int i = 0; i < corridors.Count; i++) {
            corridors[i] = IncreaseCoorridorBrush3by3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
     }

     private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions) {
        foreach (var position in deadEnds) {
            if(roomPositions.Contains(position) == false) {
                var roomFloor = RunRandomWalk(randomWalkParameters, position);
                roomPositions.UnionWith(roomFloor);
            }
        }
     }

     private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions) {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions) {
            int neighborsCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList) {
                if(floorPositions.Contains(position + direction)) {
                    neighborsCount++;
                }
            }

            if(neighborsCount == 1) {
                deadEnds.Add(position);
            }
        }

        return deadEnds;
     }

     private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions) {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        for(int i = 0; i < corridorCount; i++) {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
     }

     private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions) {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList(); 
        foreach(var roomPosition in roomToCreate) {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        

        return roomPositions;
     }

     private List<Vector2Int> IncreaseCoorridorBrush3by3 (List<Vector2Int> corridor) {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for(int i = 1; i < corridor.Count; i++) {
            for(int x = -1; x < 2; x++) {
                for (int y = -1; y < 2; y++) {
                    newCorridor.Add(corridor[i-1] + new Vector2Int(x, y));
                }
            }
        }

        return newCorridor;
     }

     /*private void ClearSpawners() {
        foreach (var enemySpawner in enemySpawners) {
            Destroy(enemySpawner);
        }
     }*/

     /*private Vector2Int GetDirection90From(Vector2Int direction) {
        if(direction == Vector2Int.up) {
            return Vector2Int.right;
        }
        if(direction == Vector2Int.right) {
            return Vector2Int.down;
        }
        if(direction == Vector2Int.down) {
            return Vector2Int.left;
        }
        if(direction == Vector2Int.left) {
            return Vector2Int.up;
        }
        return Vector2Int.zero;

     }*/
}
