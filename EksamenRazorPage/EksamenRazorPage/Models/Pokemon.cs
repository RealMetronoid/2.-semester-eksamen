namespace EksamenRazorPage.Models
{
    public class Pokemon
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        string Type1 { get; set; }
        string? Type2 { get; set; }
        List<Move> Movepool { get; set; }
        Pokemon? PreEvolution { get; set; }
        Pokemon? Evolution { get; set; }

        Pokemon(int id, string name, string type1)
        {
            Id = id;
            Name = name;
            Type1 = type1;
            Type2 = null;
            Movepool = new List<Move>();
            PreEvolution = null;
            Evolution = null;
        }

        Pokemon(int id, string name, string type1, string type2)
        {
            Id = id;
            Name = name;
            Type1 = type1;
            Type2 = type2;
            Movepool = new List<Move>();
            PreEvolution = null;
            Evolution = null;
        }

        // Methods.
        #region Pre-Evolution methods
        public void SetPreEvolution(Pokemon preEvolution)
        {
            if (PreEvolution == null)
            {
                //  Adds the PreEvolution if there was none.
                PreEvolution = preEvolution;
                preEvolution.Evolution = this;
            } else
            {
                //  Update the PreEvolution.
                PreEvolution.Evolution = null;
                PreEvolution = preEvolution;
                preEvolution.Evolution = this;
            }
        }

        public void DeletePreEvolution()
        {
            if (PreEvolution != null)
            {
                PreEvolution.Evolution = null;
                PreEvolution = null;
            }
        }
        #endregion

        #region Evolution methods
        public void SetEvolution(Pokemon evolution)
        {
            if (Evolution == null)
            {
                //  Adds the Evolution if there was none.
                Evolution = evolution;
                evolution.PreEvolution = this;
            }
            else
            {
                //  Update the PreEvolution.
                Evolution.PreEvolution = null;
                Evolution = evolution;
                evolution.PreEvolution = this;
            }
        }

        public void DeleteEvolution()
        {
            if (Evolution != null)
            {
                Evolution.Evolution = null;
                Evolution = null;
            }
        }
        #endregion

        #region Movepool Methods
        public void AddMove(Move move)
        {
            Movepool.Add(move);
        }
        public void AddMove(List<Move> moves)
        {
            foreach (Move move in moves)
            {
                Movepool.Add(move);
            }
        }

        public void DeleteMove(Move move)
        {
            try
            {
                Movepool.Remove(move);
            } catch (Exception e) { Console.WriteLine(e.Message); }
        }


        #endregion


    }
}
