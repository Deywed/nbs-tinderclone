using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Neo4j.Driver;

namespace Backend.Services.Neo4J
{
    public class Neo4JService : ISwipeService
    {
        private readonly IDriver _driver;
        public Neo4JService(IDriver driver)
        {
            _driver = driver;
        }

        public async Task BlockUserAsync(string userId, string blockedUserId)
        {
            await using var session = _driver.AsyncSession();
            await session.ExecuteWriteAsync(async tx =>
           {
               var query = @"
                MATCH (u1:User {id: $userId})
                MATCH (u2:User {id: $blockedUserId})
                MATCH (u1)-[r1:LIKES]-(u2)
                DELETE r1
                MERGE (u1)-[b:BLOCKED]->(u2)
                ON CREATE SET b.createdAt = datetime()";
               await tx.RunAsync(query, new { userId, blockedUserId });
           });
        }

        public async Task DislikeUserAsync(string userId, string dislikedUserId)
        {
            await using var session = _driver.AsyncSession();
            await session.ExecuteWriteAsync(async tx =>
            {
                var query = @"
                MERGE (u1: User {id: $userId})
                MERGE (u2: User {id: $dislikedUserId})
                MERGE (u1)-[:DISLIKES]->(u2)";
                await tx.RunAsync(query, new { userId, dislikedUserId });
            }
            );
        }

        public Task<List<string>> GetMatchesByUserIdAsync(string userId)
        {
            var session = _driver.AsyncSession();
            return session.ExecuteReadAsync(async tx =>
            {
                var query = @"
                MATCH (u: User {id: $userId})-[:LIKES]->(liked: User)
                MATCH (liked)-[:LIKES]->(u) 
                RETURN liked.id AS likedUserId";
                var result = await tx.RunAsync(query, new { userId });

                var matchedUserIds = new List<string>();
                while (await result.FetchAsync())
                {
                    matchedUserIds.Add(result.Current["likedUserId"].As<string>());
                }
                return matchedUserIds;
            });
        }

        public async Task<bool> LikeUserAsync(string userId, string likedUserId)
        {
            await using var session = _driver.AsyncSession();

            return await session.ExecuteWriteAsync(async tx =>
            {
                var query = @"
                    MERGE (u1:User {id: $userId})
                    MERGE (u2:User {id: $likedUserId})
                    MERGE (u1)-[r:LIKES]->(u2)
                    ON CREATE SET r.createdAt = datetime()
                    WITH u1, u2
                    OPTIONAL MATCH (u2)-[r2:LIKES]->(u1)
                    RETURN r2 IS NOT NULL AS isMatch";
                var result = await tx.RunAsync(query, new { userId, likedUserId });

                if (await result.FetchAsync())
                {
                    return result.Current["isMatch"].As<bool>();
                }
                return false;
            }
            );
        }

        public async Task RemoveMatchAsync(string userId, string matchedUserId)
        {
            await using var session = _driver.AsyncSession();
            await session.ExecuteWriteAsync(async tx =>
            {
                var query = @"
                MATCH (u1: User {id: $userId})-[r1:LIKES]->(u2: User {id: $matchedUserId})
                MATCH (u2)-[r2:LIKES]->(u1)
                DELETE r1, r2";
                await tx.RunAsync(query, new { userId, matchedUserId });
            }
            );
        }
    }
}