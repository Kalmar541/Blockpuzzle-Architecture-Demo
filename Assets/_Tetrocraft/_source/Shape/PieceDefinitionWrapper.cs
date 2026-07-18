namespace GameProject.Game
{
    public static class PieceDefinitionWrapper 
    {
        public static ShapeDefinition CastPieceDefinition(ShapeBluePrint pieceBluePrint)
        {
            Position[] positions = new Position[pieceBluePrint.Positions.Length];
            for (int i = 0; i < pieceBluePrint.Positions.Length; i++)
            {
                positions[i] = new Position(pieceBluePrint.Positions[i].x, pieceBluePrint.Positions[i].y);
            }
            return new(positions, pieceBluePrint.ShapeType, pieceBluePrint.IsCanRotate);
        }
    }
}