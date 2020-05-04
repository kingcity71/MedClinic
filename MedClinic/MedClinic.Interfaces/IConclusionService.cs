using MedClinic.Model.Conclusion;
using System;

namespace MedClinic.Interfaces
{
    public interface IConclusionService
    {
        public ConclusionModel GetConclusion(Guid id);
        public ConclusionModel GetConclusionBySched(Guid schedId);
        public ConclusionModel Create(ConclusionModel conclusion);
        public ConclusionModel Update(ConclusionModel conclusion);
        public void Remove(Guid id);
    }
}
