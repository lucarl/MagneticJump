using UnityEngine;

namespace DefaultNamespace
{
    public class JellyVertex
    {
        public int VertexIndex;
        public Vector3 InitialVertexPosition;
        public Vector3 CurrentVertexPosition;

        public Vector3 CurrentVelocity;

        public JellyVertex(int vertexIndex, Vector3 initialVertexPosition, Vector3 currentVertexPosition,
            Vector3 currentVelocity)
        {
            this.VertexIndex = vertexIndex;
            this.InitialVertexPosition = initialVertexPosition;
            this.CurrentVertexPosition = currentVertexPosition;
            this.CurrentVelocity = currentVelocity;
        }

        private Vector3 GetCurrentDisplacement()
        {
            return CurrentVertexPosition - InitialVertexPosition;
        }

        //Updating the velocity to each vertex 
        public void UpdateVelocity(float bounceSpeed)
        {
            CurrentVelocity -= GetCurrentDisplacement() * (bounceSpeed * Time.deltaTime);
        }

        //Stop the jiggle over time
        public void Settle(float stiffness)
        {
            CurrentVelocity *= 1f - stiffness * Time.deltaTime;
        }
        
        public void ApplyPressureToVertex(Transform transform, Vector3 position, float pressure)
        {
            Vector3 distanceVertexPoint = CurrentVertexPosition - transform.InverseTransformPoint(position);
            float adaptedPressure = pressure / (1f + distanceVertexPoint.sqrMagnitude);
            float velocity = adaptedPressure * Time.deltaTime;
            CurrentVelocity += distanceVertexPoint.normalized * velocity;
        }
    }
}