namespace HockeyTeam.Migrations
{
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HockeyTeam.DAL.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "HockeyTeam.DAL.StoreContext";
        }

        protected override void Seed(HockeyTeam.DAL.StoreContext context)
        {
            var positions = new List<Position>
            {
                new Position { Name = "Forwards" },
                new Position { Name = "Defense" },
                new Position { Name = "Goalies" },
                new Position { Name = "Coaches" },
                new Position { Name= "Marketing" },
                new Position { Name= "Management" }
            };
            positions.ForEach(c => context.Positions.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();

            var players = new List<Player>
            {
                new Player { Name = "Alexander Barabanov", Description="Signed as a free agent by Toronto, April 7, 2020", Score=11.46M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Pierre Engvall", Description="Signed a contract with the Maple Leafs on May 18, 2018", Score=2.12M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Alex Galchenyuk", Description="Alex had 31 goals and 83 points in his first season, drawing notice from NHL teams", Score=15.93M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Zach Hyman", Description="Named a First Team All-America and a Hobey Baker Award finalist", Score=2.35M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Alexander Kerfoot", Description="Saw the Colorado Avalanche as his quickest route to the NHL", Score=24.75M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Mitchell Marner", Description="He was selected by Toronto with the No. 4 pick in the 2015 NHL Draft", Score=8.82M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Auston Matthews", Description="He excelled at every level of the game on his unique path to the NHL", Score=9.56M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Ilya Mikheyev", Description="He was an instant hit in the NHL after signing with the Toronto Maple Leafs on May 6, 2019", Score=19.24M, PositionID= positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "William Nylander", Description="His father played 920 games with seven NHL teams", Score=4.84M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Wayne Simmonds", Description="All of the goals he has scored only adds to his popularity", Score=4.24M, PositionID=positions.Single(c => c.Name == "Forwards").ID },

                new Player { Name = "Jason Spezza", Description="Spezza's career came full circle on July 1, 2019", Score=24.74M, PositionID= positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "John Tavares", Description="He had NHL career highs in goals (47) and points (88) in his first season", Score=49.14M, PositionID= positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Joe Thornton", Description="Thornton was acquired by the San Jose Sharks on Nov. 30, 2005", Score=75.24M, PositionID=positions.Single( c => c.Name == "Forwards").ID },

                new Player { Name = "Zach Bogosian", Description="Bogosian's NHL career took a 180-degree turn in February 2020", Score=35.96M, PositionID= positions.Single( c => c.Name == "Defense").ID },

                new Player { Name = "TJ Brodie", Description="Brodie was selected by the Flames in the fourth round (No. 114) of the 2008 NHL Draft", Score=13.49M, PositionID=positions.Single( c => c.Name == "Defense").ID },

                new Player { Name = "Travis Dermott", Description="Dermott signed a three-year, entry-level contract with the Maple Leafs on July 22, 2015", Score=29.46M, PositionID=positions.Single( c => c.Name == "Defense").ID },

                new Player { Name = "Justin Holl", Description="Holl played most of the 2014-15 with Indy of the ECHL", Score=3.53M, PositionID=positions.Single( c => c.Name == "Defense").ID },

                new Player { Name = "Jake Muzzin", Description="Selected him in the fifth round (No. 141) of the 2007 NHL Draft", Score=5.45M, PositionID=positions.Single( c => c.Name == "Defense").ID },

                new Player { Name = "Morgan Rielly", Description="Rielly entered the NHL with the Toronto Maple Leafs to much fanfare", Score=12.20M, PositionID=positions.Single( c => c.Name == "Defense").ID },

                new Player { Name = "Rasmus Sandin", Description="Assigned to Toronto of the American Hockey League for 2018-19", Score=35.96M, PositionID=positions.Single( c => c.Name == "Defense").ID },

                new Player { Name = "Brendan Shanahan", Description="Former player who currently serves as the president and alternate governor", Score=5.64M, PositionID=positions.Single( c => c.Name == "Management").ID },

                new Player { Name = "Kyle Dubas", Description="Currently the general manager of the Toronto Maple", Score=25.14M, PositionID=positions.Single( c => c.Name == "Management").ID },

                new Player { Name = "Sheldon Keefe", Description="Head coach of the Toronto Maple Leafs", Score=35.96M, PositionID=positions.Single( c => c.Name == "Coaches").ID },

                new Player { Name = "Manny Malhotra", Description="He last played with the Lake Erie Monsters in the American Hockey League", Score=5.14M, PositionID=positions.Single( c => c.Name == "Coaches").ID },

                new Player { Name = "Frederik Andersen", Description="He had a goals-against average of 2.45 in 22 games", Score=21.06M, PositionID=positions.Single( c => c.Name == "Goalies").ID },

                new Player { Name = "Jack Campbell", Description="Chosen by the Dallas Stars with the No. 11 pick in the 2010 NHL Draft", Score=24.01M, PositionID=positions.Single( c => c.Name == "Goalies").ID },

                new Player { Name = "Alison Rockwell", Description="Business Relations at Toronto Maple Leafs", Score=3.51M, PositionID=positions.Single( c => c.Name == "Marketing").ID },

                new Player { Name = "Sirpa Lehti", Description="Assistant, Video Analyst", Score=2.56M, PositionID=positions.Single( c => c.Name == "Marketing").ID }
            };
            players.ForEach(c => context.Players.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();
        }
    }
}
